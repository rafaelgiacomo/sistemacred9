using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.Security;
using SistemaCred9.Web.UI.ViewModels.Usuario;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {
        
        public UsuarioController()
        {

        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        public ActionResult Index()
        {
            var viewModel = new UsuarioIndexViewModel();

            viewModel.Usuarios = new System.Collections.Generic.List<UsuarioViewModel>()
            {
                new UsuarioViewModel()
                {
                    Id = 1,
                    Nome = "Rafael",
                    Email = "rafael.giacomo@cred9.com.br",
                    NomeUsuario = "giacomo"
                }
            };

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        public ActionResult Adicionar()
        {
            var viewModel = new UsuarioViewModel();

            viewModel.TipoUsuario = new SelectList(TipoUsuarioModelo.ListarTodos(), "Id", "Descricao");

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        public ActionResult Editar()
        {
            var viewModel = new UsuarioViewModel();

            viewModel.TipoUsuario = new SelectList(TipoUsuarioModelo.ListarTodos(), "Id", "Descricao");

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.Coordenador)]
        public ActionResult Excluir()
        {
            var viewModel = new UsuarioViewModel();

            return View(viewModel);
        }
    }
}