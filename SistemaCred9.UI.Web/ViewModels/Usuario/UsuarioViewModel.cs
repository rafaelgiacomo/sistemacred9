using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.ViewModels.Shared;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.ViewModels.Usuario
{
    public class UsuarioViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string NomeUsuario { get; set; }

        [Required]
        public string Senha { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Senha")]
        [Required(ErrorMessage = "O campor confirmar senha é obrigatório")]
        public string ConfirmarSenha { get; set; }

        [Display(Name = "Tipo de Usuário")]
        public SelectList TipoUsuario { get; set; }

        [Required(ErrorMessage = "Selecione um tipo de usuario")]
        public int TipoUsuarioId { get; set; }

        public string TipoUsuarioDescricao { get { return ((TipoUsuarioEnum)TipoUsuarioId).ToString(); } }

        public UsuarioViewModel() : base()
        {

        }
    }
}