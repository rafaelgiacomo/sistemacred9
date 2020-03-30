using SistemaCred9.Web.UI.ViewModels.Shared;
using System.ComponentModel.DataAnnotations;

namespace SistemaCred9.Web.UI.ViewModels.Especie
{
    public class EspecieViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Espécie")]
        [DataType(DataType.Text)]
        public int? CodEspecie { get; set; }

        public EspecieViewModel() : base()
        {

        }
    }
}