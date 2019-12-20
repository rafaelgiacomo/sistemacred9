using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.ViewModels.Venda;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class VendaController : BaseController
    {
        public VendaController()
        {
        }

        public ActionResult Index(int? statusId)
        {
            var viewModel = new VendaIndexViewModel();

            if (!statusId.HasValue)
                statusId = (int) VendaStatusEnum.AguardandoDocumento;

            return View(viewModel);
        }

        public ActionResult Adicionar()
        {
            return View();
        }
    }
}