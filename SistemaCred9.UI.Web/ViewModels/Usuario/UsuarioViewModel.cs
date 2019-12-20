using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.ViewModels.Usuario
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string NomeUsuario { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campor confirmar senha é obrigatório")]
        public string ConfirmarSenha { get; set; }

        [Display(Name = "Tipo de Usuário")]
        public SelectList TipoUsuario { get; set; }

        [Required(ErrorMessage = "Selecione um tipo de usuario")]
        public int TipoUsuarioSelecionado { get; set; }
    }
}