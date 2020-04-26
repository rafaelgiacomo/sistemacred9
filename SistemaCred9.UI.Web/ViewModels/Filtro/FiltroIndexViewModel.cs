using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Filtro
{
    public class FiltroIndexViewModel : BaseViewModel
    {
        public List<ContratoRelatorioViewModel> Filtros { get; set; }

        public FiltroIndexViewModel() : base()
        {
            Filtros = new List<ContratoRelatorioViewModel>();
        }
    }
}