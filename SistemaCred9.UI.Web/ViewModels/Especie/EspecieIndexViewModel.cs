using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Especie
{
    public class EspecieIndexViewModel : BaseViewModel
    {
        public List<EspecieViewModel> Especies { get; set; }

        public EspecieIndexViewModel() : base()
        {
            Especies = new List<EspecieViewModel>();
        }
    }
}