using SistemaCred9.Web.UI.ViewModels.Shared;
using System.ComponentModel.DataAnnotations;

namespace SistemaCred9.Web.UI.ViewModels.TabelaComissao
{
    public class TabelaComissaoViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Valor Comissão")]
        public float ValorComissao { get; set; }

        public TabelaComissaoViewModel() : base()
        {

        }
    }
}