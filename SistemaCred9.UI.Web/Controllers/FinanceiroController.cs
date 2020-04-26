using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    public class FinanceiroController : BaseController
    {
        public FinanceiroController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
        }

        // GET: Financeiro
        public ActionResult Index()
        {
            return View();
        }
    }
}