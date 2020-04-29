using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Financeiro
{
    public class FinanceiroIndexViewModel : BaseViewModel
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public List<ContratoRelatorioViewModel> ListaContratos { get; set; }

        public FinanceiroIndexViewModel()
        {
            ListaContratos = new List<ContratoRelatorioViewModel>();
        }

    }
}