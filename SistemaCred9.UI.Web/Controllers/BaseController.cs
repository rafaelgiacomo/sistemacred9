using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Infra;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.ViewModels.Shared;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    public class BaseController : Controller, IDisposable
    {
        public readonly string _connectionString = ConfigurationManager.ConnectionStrings["DbPanorama"].ConnectionString;

        public readonly UsuarioNegocio _usuarioNegocio;
        public readonly VendaNegocio _vendaNegocio;
        public readonly TarefaNegocio _tarefaNegocio;
        public readonly UnitOfWork _unitOfWork;
        public readonly RepositorioPanorama.UnitOfWork _unitOfWorkPanorama;

        public BaseController()
        {
            _unitOfWork = new UnitOfWork(new Cred9DbContext());

            _usuarioNegocio = new UsuarioNegocio(_unitOfWork);
            _vendaNegocio = new VendaNegocio(_unitOfWork);
            _tarefaNegocio = new TarefaNegocio(_unitOfWorkPanorama, _unitOfWork);
        }

        public ActionResult Erro()
        {
            var viewModel = new BaseViewModel();

            ViewBag.mensagem = TempData["mensagem"];
            return View(viewModel);
        }

        void IDisposable.Dispose()
        {
        }
    }
}