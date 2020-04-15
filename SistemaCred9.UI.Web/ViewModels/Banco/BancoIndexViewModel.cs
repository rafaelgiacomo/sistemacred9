using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Banco
{
    public class BancoIndexViewModel : BaseViewModel
    {
        public List<BancoViewModel> Bancos { get; set; }

        public BancoIndexViewModel() : base()
        {
            Bancos = new List<BancoViewModel>();
        }
    }
}