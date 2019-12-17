using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    public class BaseController : Controller, IDisposable
    {
        public readonly string _connectionString = ConfigurationManager.ConnectionStrings["DbCondVagas"].ConnectionString;

        public BaseController()
        {
        }

        public ActionResult Erro()
        {
            ViewBag.mensagem = TempData["mensagem"];
            return View();
        }

        void IDisposable.Dispose()
        {
        }
    }
}