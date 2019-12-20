using System;
using System.Web.Mvc;
using System.Web.Security;

namespace SistemaCred9.Web.UI.Controllers
{
    [AllowAnonymous]
    public class ContaController : BaseController
    {

        //private readonly UsuarioBusiness _usuarioBus;

        public ContaController()
        {
            //_usuarioBus = new UsuarioBusiness(_connectionString);
        }

        public ActionResult LogOn()
        {
            ViewBag.Mensagem = "";
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(FormCollection form, string returnUrl)
        {
            try
            {
                //UsuarioModel usuario = _usuarioBus.SelecionarPorLogin(form["Login"]);
                //UsuarioModel usuario = null;

                //if (usuario != null)
                //{
                //    if (usuario.VerificarSenha(form["Senha"], usuario.Senha))
                //    {
                //        FormsAuthentication.SetAuthCookie(usuario.Login, false);

                //        if (Url.IsLocalUrl(returnUrl)
                //            && returnUrl.Length > 1
                //            && returnUrl.StartsWith("/")
                //            && !returnUrl.StartsWith("//")
                //            && !returnUrl.StartsWith("/\\"))
                //        {
                //            return Redirect(returnUrl);
                //        }
                FormsAuthentication.SetAuthCookie(form["Login"], false);
                return RedirectToAction("Index", "Home");
                //    }
                //}

                //ViewBag.Mensagem = "Login ou Senha inválido";
                //return View();
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            //FormsAuthentication.GetAuthCookie()
            return RedirectToAction("LogOn");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}