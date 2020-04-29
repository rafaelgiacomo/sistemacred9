using SistemaCred9.Core.Dto;
using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.ViewModels.Financeiro;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Index()
        {
            var dataAtual = DateTime.Now;
            var viewModel = new FinanceiroIndexViewModel();

            viewModel.Ano = dataAtual.Year;
            viewModel.Mes = dataAtual.Month;

            return View(viewModel);
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

                        var response = _contratoNegocio.RealizarImportacao((TipoPlanilhaEnum)viewModel.TipoPlanilhaId, caminho);

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

        public ActionResult ErroImportar()
        {
            var listaErro = (List<ContratoRelatorioErroDto>) TempData["ContratosErro"];
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