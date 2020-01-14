using SistemaCred9.Web.UI.ViewModels.Shared;
using System.ComponentModel.DataAnnotations;

namespace SistemaCred9.Web.UI.ViewModels.Usuario
{
    public class TrocarSenhaViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Nova Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campor confirmar senha é obrigatório")]
        [Display(Name = "Confirmar Nova Senha")]
        public string ConfirmarSenha { get; set; }

        public TrocarSenhaViewModel() : base()
        {

        }

    }
}