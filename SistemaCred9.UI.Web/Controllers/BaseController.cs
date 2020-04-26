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
            _unitOfWorkPanorama = new RepositorioPanorama.UnitOfWork(_connectionString, new LoggerConsole());

            _usuarioNegocio = new UsuarioNegocio(_unitOfWork);
            _vendaNegocio = new VendaNegocio(_unitOfWork);
            _tarefaNegocio = new TarefaNegocio(_unitOfWorkPanorama, _unitOfWork);
        }

        public ActionResult Erro()
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

            ViewBag.mensagem = TempData["mensagem"];
            return View(viewModel);
        }

        void IDisposable.Dispose()
        {
        }
    }
}