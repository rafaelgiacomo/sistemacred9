using AutoMapper;
using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.Security;
using SistemaCred9.Web.UI.ViewModels.Venda;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class VendaController : BaseController
    {
        private readonly VendaNegocio _vendaNegocio;
        private readonly UsuarioNegocio _usuarioNegocio;
        private readonly TarefaNegocio _tarefaNegocio;

        public VendaController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
            var unitOfWorkPanorama = new RepositorioPanorama.UnitOfWork(_connectionString);

            _vendaNegocio = new VendaNegocio(unitOfWork);
            _usuarioNegocio = new UsuarioNegocio(unitOfWork);
            _tarefaNegocio = new TarefaNegocio(unitOfWorkPanorama, unitOfWork);
        }

        [PermissoesFiltro(Roles = Role.VENDA_INDEX)]
        public ActionResult Index(int? statusId)
        {
            var viewModel = new VendaIndexViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            if (!statusId.HasValue)
                statusId = (int) VendaStatusEnum.AguardandoDocumento;

            

            viewModel.StatusAtual = ((VendaStatusEnum)statusId).ToString();
            viewModel.Vendas = Mapper.Map<List<VendaViewModel>>(_vendaNegocio.ListarVendasPorStatus(statusId.Value, usuarioId));
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.VENDA_ADICIONAR)]
        public ActionResult Adicionar()
        {
            var viewModel = new VendaViewModel();
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

        [HttpPost]
        public ActionResult Adicionar(VendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
                var entidade = Mapper.Map<Venda>(viewModel);
                entidade.StatusId = (int)VendaStatusEnum.AguardandoDocumento;
                entidade.Data = DateTime.Now;
                entidade.UsuarioId = usuario.Id;

                entidade.Historico = new List<VendaStatusHistorico>()
                {
                    new VendaStatusHistorico()
                    {
                        Data = entidade.Data,
                        StatusId = (int)VendaStatusEnum.AguardandoDocumento,
                        UsuarioId = usuario.Id,
                        Venda = entidade,
                        Observacao = viewModel.Observacao
                    }
                };

                _vendaNegocio.Adicionar(entidade);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível adicionar Venda.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.VENDA_MUDAR_STATUS)]
        public ActionResult MudarStatus(int entidadeId)
        {
            var viewModel = new VendaStatusHistoricoViewModel();
            var vendaViewModel = Mapper.Map<VendaViewModel>(_vendaNegocio.Obter(entidadeId));
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId != (int)TipoUsuarioEnum.Administrador
                && usuario.TipoUsuarioId != (int)TipoUsuarioEnum.Coordenador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.VendaId = entidadeId;
            viewModel.Venda = vendaViewModel;
            viewModel.ListaStatus = new SelectList(VendaStatusModelo.ListarTodos(), "Id", "Descricao");
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult MudarStatus(VendaStatusHistoricoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<VendaStatusHistorico>(viewModel);
                var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);

                entidade.Data = DateTime.Now;
                entidade.UsuarioId = usuario.Id;

                _vendaNegocio.AdicionarStatus(entidade);

                return RedirectToAction("Index", new { });
            }

            return View(viewModel);
        }

        public ActionResult Ver(int entidadeId)
        {
            var venda = _vendaNegocio.Obter(entidadeId);
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            if (venda == null)
            {
                ViewBag.Mensagem = "Não foi possível adicionar o Usuario.";
                return RedirectToAction("Erro");
            }

            var viewModel = Mapper.Map<VendaViewModel>(venda);
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }
    }
}