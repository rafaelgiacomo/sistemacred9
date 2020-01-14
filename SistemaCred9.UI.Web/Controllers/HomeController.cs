using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly VendaNegocio _vendaNegocio;
        private readonly UsuarioNegocio _usuarioNegocio;
        private readonly TarefaNegocio _tarefaNegocio;

        public HomeController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
            var unitOfWorkPanorama = new RepositorioPanorama.UnitOfWork(_connectionString);

            _vendaNegocio = new VendaNegocio(unitOfWork);
            _usuarioNegocio = new UsuarioNegocio(unitOfWork);
            _tarefaNegocio = new TarefaNegocio(unitOfWorkPanorama, unitOfWork);
        }

        public ActionResult Index()
        {
            var viewModel = new BaseViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        public ActionResult Negado()
        {
            var viewModel = new BaseViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }
    }
}