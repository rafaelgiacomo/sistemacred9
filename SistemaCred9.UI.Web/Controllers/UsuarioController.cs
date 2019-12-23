using AutoMapper;
using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.Security;
using SistemaCred9.Web.UI.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly UsuarioNegocio _usuarioNegocio;

        public UsuarioController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
            _usuarioNegocio = new UsuarioNegocio(unitOfWork);
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        public ActionResult Index()
        {
            var viewModel = new UsuarioIndexViewModel();
            viewModel.Usuarios = Mapper.Map<List<UsuarioViewModel>>(_usuarioNegocio.ListarTodos());

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        public ActionResult Adicionar()
        {
            var viewModel = new UsuarioViewModel();
            viewModel.TipoUsuario = new SelectList(TipoUsuarioModelo.ListarTodos(), "Id", "Descricao");

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Adicionar(UsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<Usuario>(viewModel);
                entidade.DefinirSenha(entidade.Senha);
                _usuarioNegocio.Adicionar(entidade);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível adicionar o Usuario.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        public ActionResult Editar(int entidadeId)
        {
            var entidade = _usuarioNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<UsuarioViewModel>(entidade);

            viewModel.TipoUsuario = new SelectList(TipoUsuarioModelo.ListarTodos(), "Id", "Descricao");

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<Usuario>(viewModel);
                entidade.DefinirSenha(entidade.Senha);
                _usuarioNegocio.Atualizar(entidade);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível atualizar dados do Usuario.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        public ActionResult Excluir(int entidadeId)
        {
            var entidade = _usuarioNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<UsuarioViewModel>(entidade);

            return View(viewModel);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int entidadeId)
        {
            try
            {
                _usuarioNegocio.Deletar(entidadeId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

        public ActionResult TrocarSenha()
        {
            try
            {
                //Recupera Usuario Logado
                var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);

                if (usuario == null)
                {
                    TempData["mensagem"] = "Ocorreu um erro ao carrgar dados.";
                    return RedirectToAction("Erro");
                }

                TrocarSenhaViewModel viewModel =
                    Mapper.Map<Usuario, TrocarSenhaViewModel>(usuario);

                viewModel.Senha = string.Empty;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrocarSenha(TrocarSenhaViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario entidade = _usuarioNegocio.SelecionarPorId(viewModel.Id);

                    entidade.DefinirSenha(viewModel.Senha);

                    _usuarioNegocio.Atualizar(entidade);

                    return RedirectToAction("SenhaAlterada");
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

    }
}