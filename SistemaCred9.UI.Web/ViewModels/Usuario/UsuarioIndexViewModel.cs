using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Usuario
{
    public class UsuarioIndexViewModel
    {
        public List<UsuarioViewModel> Usuarios { get; set; }

        public UsuarioIndexViewModel()
        {
            Usuarios = new List<UsuarioViewModel>();
        }
    }
}