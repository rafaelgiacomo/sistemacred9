using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.TabelaComissao
{
    public class TabelaComissaoIndexViewModel : BaseViewModel
    {
        public List<TabelaComissaoViewModel> ListaTabelaComissao { get; set; }

        public TabelaComissaoIndexViewModel() : base()
        {

        }
    }
}