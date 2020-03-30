using AutoMapper;
using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.Security;
using SistemaCred9.Web.UI.ViewModels.Banco;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class BancoController : BaseController
    {
        private readonly FiltroBancoNegocio _bancoNegocio;

        public BancoController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());

            _bancoNegocio = new FiltroBancoNegocio(unitOfWork);
        }

        [PermissoesFiltro(Roles = Role.BANCO_INDEX)]
        public ActionResult Index()
        {
            var viewModel = new BancoIndexViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.Bancos = Mapper.Map<List<BancoViewModel>>(_bancoNegocio.ListarTodos());
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.BANCO_ADICIONAR)]
        public ActionResult Adicionar()
        {
            var viewModel = new BancoViewModel();
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

        [HttpPost]
        public ActionResult Adicionar(BancoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<FiltroBanco>(viewModel);
                _bancoNegocio.Adicionar(entidade);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível adicionar Espécie.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.ESPECIE_EDITAR)]
        public ActionResult Editar(int entidadeId)
        {
            var entidade = _bancoNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<BancoViewModel>(entidade);
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

        [HttpPost]
        public ActionResult Editar(BancoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<FiltroBanco>(viewModel);
                _bancoNegocio.Atualizar(entidade);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível atualizar dados da Especie.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.ESPECIE_EXCLUIR)]
        public ActionResult Excluir(int entidadeId)
        {
            var entidade = _bancoNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<BancoViewModel>(entidade);
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
                _bancoNegocio.Deletar(entidadeId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

    }
}