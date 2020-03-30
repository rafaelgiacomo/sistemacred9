using AutoMapper;
using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.Security;
using SistemaCred9.Web.UI.ViewModels.Shared;
using SistemaCred9.Web.UI.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {
        public UsuarioController()
        {
        }

        [PermissoesFiltro(Roles = Role.USUARIO_INDEX)]
        public ActionResult Index()
        {
            var viewModel = new UsuarioIndexViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.Usuarios = Mapper.Map<List<UsuarioViewModel>>(_usuarioNegocio.ListarTodosSemAdm());
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.USUARIO_ADICIONAR)]
        public ActionResult Adicionar()
        {
            var viewModel = new UsuarioViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.TipoUsuario = new SelectList(TipoUsuarioModelo.ListarTodos(), "Id", "Descricao");
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

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

        [PermissoesFiltro(Roles = Role.USUARIO_EDITAR)]
        public ActionResult Editar(int entidadeId)
        {
            var entidade = _usuarioNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<UsuarioViewModel>(entidade);
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.TipoUsuario = new SelectList(TipoUsuarioModelo.ListarTodos(), "Id", "Descricao");
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

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

        [PermissoesFiltro(Roles = Role.USUARIO_EXCLUIR)]
        public ActionResult Excluir(int entidadeId)
        {
            var entidade = _usuarioNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<UsuarioViewModel>(entidade);
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

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
                int? usuarioId = null;

                if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
                {
                    usuarioId = usuario.Id;
                }

                if (usuario == null)
                {
                    TempData["mensagem"] = "Ocorreu um erro ao carrgar dados.";
                    return RedirectToAction("Erro");
                }

                TrocarSenhaViewModel viewModel =
                    Mapper.Map<Usuario, TrocarSenhaViewModel>(usuario);

                viewModel.Login = usuario.NomeUsuario;
                viewModel.Senha = string.Empty;
                viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
                viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

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
                var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
                int? usuarioId = null;

                if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
                {
                    usuarioId = usuario.Id;
                }

                if (ModelState.IsValid)
                {
                    Usuario entidade = _usuarioNegocio.SelecionarPorId(viewModel.Id);

                    entidade.DefinirSenha(viewModel.Senha);

                    _usuarioNegocio.Atualizar(entidade);

                    return RedirectToAction("SenhaAlterada");
                }

                viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
                viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

        public ActionResult SenhaAlterada()
        {
            var viewModel = new BaseViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId != (int)TipoUsuarioEnum.Administrador
                && usuario.TipoUsuarioId != (int)TipoUsuarioEnum.Coordenador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

    }
}