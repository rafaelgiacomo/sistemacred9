﻿using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}