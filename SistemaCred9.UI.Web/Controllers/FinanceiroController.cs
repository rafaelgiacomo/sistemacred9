using AutoMapper;
using SistemaCred9.Core.Dto;
using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.ViewModels.Financeiro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    public class FinanceiroController : BaseController
    {
        private readonly ContratoRelatorioNegocio _contratoNegocio;

        public FinanceiroController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
            _contratoNegocio = new ContratoRelatorioNegocio(unitOfWork);
        }

        public ActionResult Index(int? menu)
        {
            try
            {
                var dataAtual = DateTime.Now;
                var viewModel = new FinanceiroIndexViewModel();

                if (!menu.HasValue)
                {
                    menu = 1;
                }

                viewModel.Menu = menu.Value;

                if (viewModel.Menu != 4)
                {
                    viewModel.ListaContratos = Mapper.Map<List<ContratoRelatorioViewModel>>(_contratoNegocio.ListarContratosPorMes(menu.Value != 2, menu.Value == 3));
                }
                else
                {
                    viewModel.ListaContratoPagamento = Mapper.Map<List<ContratoPagamentoViewModel>>(_contratoNegocio.ListarPagamentosSemContrato());
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

        public ActionResult Importar()
        {
            var viewModel = new ImportarViewModel();

            return View(viewModel);
        }

        public ActionResult ValidarContratos()
        {
            try
            {
                var contratos = _contratoNegocio.ListarContratosPorMes(true, false);
                var response = _contratoNegocio.ValidarContratos(contratos.Select(x => x.Id).ToList());

                if (response.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["mensagem"] = response.FriendlyMessage;
                    return RedirectToAction("Erro");
                }                
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Importar(ImportarViewModel viewModel)
        {
            try
            {
                foreach (var file in viewModel.Files)
                {
                    if (file.ContentLength > 0)
                    {
                        string nome = Path.GetFileNameWithoutExtension(file.FileName);
                        string tipo = Path.GetExtension(file.FileName);
                        string caminho = Path.Combine(Server.MapPath("~/Content/Uploads"), nome + tipo);

                        file.SaveAs(caminho);

                        var response = _contratoNegocio.RealizarImportacao((TipoPlanilhaEnum)viewModel.TipoPlanilhaId, caminho, file.FileName);

                        if (!response.Success)
                        {
                            TempData["ContratosErro"] = response.Data;
                            return RedirectToAction("ErroImportar");
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

        public ActionResult VerContrato(int entidadeId)
        {
            try
            {
                var response = _contratoNegocio.Obter(entidadeId);

                if (response.Success)
                {
                    var viewModel = new VerContratoViewModel();

                    viewModel.Contrato = response.Data.Contrato;
                    viewModel.Cpf = response.Data.Cpf;
                    viewModel.NomeCliente = response.Data.NomeCliente;
                    viewModel.DataLancamento = response.Data.DataLancamento.HasValue ? response.Data.DataLancamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                    viewModel.NomeAssessor = response.Data.NomeAssessor;

                    viewModel.ListaContratoPagamento =
                                            Mapper.Map<List<ContratoPagamentoViewModel>>(_contratoNegocio.ListarPagamentosDeContrato(viewModel.Contrato));

                    return View(viewModel);
                }
                else
                {
                    TempData["mensagem"] = "Não foi possível encontrar registro";
                    return RedirectToAction("Erro");
                }
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

        public ActionResult ErroImportar()
        {
            var listaErro = (List<ContratoRelatorioErroDto>)TempData["ContratosErro"];
            FinanceiroErroImportarViewModel viewModel = null;

            if (listaErro == null)
            {
                viewModel = new FinanceiroErroImportarViewModel();
            }
            else
            {
                viewModel = new FinanceiroErroImportarViewModel(listaErro);
            }

            return View(viewModel);
        }

    }
}