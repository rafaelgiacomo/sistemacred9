using SistemaCred9.Core.Dto;
using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Financeiro
{
    public class FinanceiroErroImportarViewModel : BaseViewModel
    {
        public List<ContratoRelatorioErroDto> ListaContratos { get; set; }

        public FinanceiroErroImportarViewModel()
        {
            ListaContratos = new List<ContratoRelatorioErroDto>();
        }

        public FinanceiroErroImportarViewModel(List<ContratoRelatorioErroDto> lista)
        {
            ListaContratos = lista;
        }
    }
}