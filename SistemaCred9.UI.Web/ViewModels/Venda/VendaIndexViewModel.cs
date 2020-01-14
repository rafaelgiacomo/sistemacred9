using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Venda
{
    public class VendaIndexViewModel : BaseViewModel
    {
        public string StatusAtual { get; set; }
        public List<VendaViewModel> Vendas { get; set; }

        public VendaIndexViewModel() : base()
        {
            Vendas = new List<VendaViewModel>();
        }
    }
}