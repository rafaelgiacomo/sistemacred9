using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Usuario
{
    public class UsuarioIndexViewModel : BaseViewModel
    {
        public List<UsuarioViewModel> Usuarios { get; set; }

        public UsuarioIndexViewModel() : base()
        {
            Usuarios = new List<UsuarioViewModel>();
        }
    }
}