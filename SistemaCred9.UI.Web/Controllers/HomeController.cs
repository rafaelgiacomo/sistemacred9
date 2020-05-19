using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
        }

        public ActionResult Index()
        {
            var viewModel = new BaseViewModel();

            return View(viewModel);
        }

        public ActionResult Negado()
        {
            var viewModel = new BaseViewModel();
            
            return View(viewModel);
        }
    }
}