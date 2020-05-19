using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Financeiro
{
    public class FinanceiroIndexViewModel : BaseViewModel
    {
        public int Menu { get; set; }
        public string Titulo { get; set; }
        public bool ComPagamento { get; set; }
        public int AcaoForm { get; set; }
        public List<ContratoRelatorioViewModel> ListaContratos { get; set; }
        public List<ContratoPagamentoViewModel> ListaContratoPagamento { get; set; }

        public FinanceiroIndexViewModel()
        {
            ListaContratos = new List<ContratoRelatorioViewModel>();
            ListaContratoPagamento = new List<ContratoPagamentoViewModel>();
            Menu = 1;
            ComPagamento = true;
        }

    }
}