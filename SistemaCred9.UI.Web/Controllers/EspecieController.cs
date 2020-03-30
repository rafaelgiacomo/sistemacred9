using AutoMapper;
using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.Security;
using SistemaCred9.Web.UI.ViewModels.Especie;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class EspecieController : BaseController
    {
        private readonly FiltroEspecieNegocio _especieNegocio;

        public EspecieController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());

            _especieNegocio = new FiltroEspecieNegocio(unitOfWork);
        }

        [PermissoesFiltro(Roles = Role.ESPECIE_INDEX)]
        public ActionResult Index()
        {
            var viewModel = new EspecieIndexViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.Especies = Mapper.Map<List<EspecieViewModel>>(_especieNegocio.ListarTodos());
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.ESPECIE_ADICIONAR)]
        public ActionResult Adicionar()
        {
            var viewModel = new EspecieViewModel();
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
        public ActionResult Adicionar(EspecieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<FiltroEspecie>(viewModel);
                _especieNegocio.Adicionar(entidade);

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
            var entidade = _especieNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<EspecieViewModel>(entidade);
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
        public ActionResult Editar(EspecieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<FiltroEspecie>(viewModel);
                _especieNegocio.Atualizar(entidade);

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
            var entidade = _especieNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<EspecieViewModel>(entidade);
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
                _especieNegocio.Deletar(entidadeId);
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