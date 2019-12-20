using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Venda
{
    public class VendaIndexViewModel
    {
        public List<VendaViewModel> Vendas { get; set; }

        public VendaIndexViewModel()
        {
            Vendas = new List<VendaViewModel>();
        }
    }
}