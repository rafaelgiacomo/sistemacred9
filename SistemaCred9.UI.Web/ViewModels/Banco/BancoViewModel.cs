using SistemaCred9.Web.UI.ViewModels.Shared;
using System.ComponentModel.DataAnnotations;

namespace SistemaCred9.Web.UI.ViewModels.Banco
{
    public class BancoViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descrição Banco")]
        public string Banco { get; set; }

        [Required]
        [Display(Name = "Min Valor Líquido")]
        [DataType(DataType.Text)]
        public float? MinLiquido { get; set; }

        [Required]
        [Display(Name = "Mín Valor Parcela")]
        [DataType(DataType.Text)]
        public int? MinParcela { get; set; }

        [Required]
        [Display(Name = "Prazo")]
        [DataType(DataType.Text)]
        public int? Prazo { get; set; }

        [Required]
        [Display(Name = "Min Parcelas em aberto")]
        [DataType(DataType.Text)]
        public int? MinParcelasEmAberto { get; set; }

        [Required]
        [Display(Name = "Max Parcelas em aberto")]
        [DataType(DataType.Text)]
        public int? MaxParcelasEmAberto { get; set; }

        [Required]
        [Display(Name = "Coeficiente")]
        [DataType(DataType.Text)]
        public float? Coeficiente { get; set; }

        public BancoViewModel() : base()
        {

        }
    }
}