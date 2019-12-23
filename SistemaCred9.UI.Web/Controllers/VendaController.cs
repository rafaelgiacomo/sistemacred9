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

        public VendaController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
            _vendaNegocio = new VendaNegocio(unitOfWork);
            _usuarioNegocio = new UsuarioNegocio(unitOfWork);
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        [PermissoesFiltro(Roles = Role.Operador)]
        [PermissoesFiltro(Roles = Role.BackOffice)]
        [PermissoesFiltro(Roles = Role.Administrador)]
        public ActionResult Index(int? statusId)
        {
            var viewModel = new VendaIndexViewModel();

            if (!statusId.HasValue)
                statusId = (int) VendaStatusEnum.AguardandoDocumento;

            viewModel.StatusAtual = ((VendaStatusEnum)statusId).ToString();
            viewModel.Vendas = Mapper.Map<List<VendaViewModel>>(_vendaNegocio.ListarVendasPorStatus(statusId.Value));

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        [PermissoesFiltro(Roles = Role.Operador)]
        [PermissoesFiltro(Roles = Role.BackOffice)]
        [PermissoesFiltro(Roles = Role.Administrador)]
        public ActionResult Adicionar()
        {
            return View();
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
                ViewBag.Mensagem = "Não foi possível adicionar o Usuario.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        [PermissoesFiltro(Roles = Role.Operador)]
        [PermissoesFiltro(Roles = Role.BackOffice)]
        [PermissoesFiltro(Roles = Role.Administrador)]
        public ActionResult MudarStatus()
        {
            return View();
        }
    }
}