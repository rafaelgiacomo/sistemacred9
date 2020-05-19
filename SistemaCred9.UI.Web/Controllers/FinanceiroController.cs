using AutoMapper;
using SistemaCred9.Core.Dto;
using SistemaCred9.Core.Resposta;
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
                viewModel.Menu = menu.HasValue ? menu.Value : 1;

                if (viewModel.Menu == 1)
                {
                    viewModel.Titulo = "Com pagamentos";
                    viewModel.ListaContratos = Mapper.Map<List<ContratoRelatorioViewModel>>(_contratoNegocio.ListarContratos(true, ContratoStatusEnum.Novo));
                }

                if (viewModel.Menu == 2)
                {
                    viewModel.Titulo = "Sem pagamentos";
                    viewModel.ListaContratos = Mapper.Map<List<ContratoRelatorioViewModel>>(_contratoNegocio.ListarContratos(false, ContratoStatusEnum.Novo));
                }

                if (viewModel.Menu == 3)
                {
                    viewModel.Titulo = "Validados";
                    viewModel.ListaContratos = Mapper.Map<List<ContratoRelatorioViewModel>>(_contratoNegocio.ListarContratos(null, ContratoStatusEnum.Validado));
                }

                if (viewModel.Menu == 4)
                {
                    viewModel.Titulo = "Pgtos sem contrato";
                    viewModel.ListaContratoPagamento = Mapper.Map<List<ContratoPagamentoViewModel>>(_contratoNegocio.ListarPagamentosSemContrato());
                }

                if (viewModel.Menu == 5)
                {
                    viewModel.Titulo = "Pendentes";
                    viewModel.ListaContratos = Mapper.Map<List<ContratoRelatorioViewModel>>(_contratoNegocio.ListarContratos(null, ContratoStatusEnum.PendenteAnalise));
                }
                    

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

        [HttpPost]
        public ActionResult Index(FinanceiroIndexViewModel viewModel)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                var listaSelecionados = viewModel.ListaContratos.Where(x => x.Selecionado).ToList();

                if (listaSelecionados.Count > 0)
                {
                    if (viewModel.AcaoForm == 1)
                    {
                        response = _contratoNegocio.ValidarContratos(listaSelecionados.Select(x => x.Id).ToList());
                    }

                    if (viewModel.AcaoForm == 2)
                    {
                        response = _contratoNegocio.ColocarContratosComPendencia(listaSelecionados.Select(x => x.Id).ToArray());
                    }

                    if (viewModel.AcaoForm == 3)
                    {
                        response = _contratoNegocio.Excluir(listaSelecionados.Select(x => x.Id).ToArray());
                    }

                    if (response.Success)
                    {
                        return RedirectToAction("Index", new { menu = viewModel.Menu });
                    }
                    else
                    {
                        TempData["mensagem"] = "Não foi possível completar a operação";
                        return RedirectToAction("Erro");
                    }
                }
                else
                {
                    TempData["mensagem"] = "Nnenhum item foi selecionado";
                    return RedirectToAction("Erro");
                }
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