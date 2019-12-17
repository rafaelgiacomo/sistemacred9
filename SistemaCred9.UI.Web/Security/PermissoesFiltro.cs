using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Security
{
    public class PermissoesFiltro : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            //Caso o usuário não seja autorizado, será direcionado para a página de negado
            if(filterContext.Result is HttpUnauthorizedResult)
                filterContext.HttpContext.Response.Redirect("/Home/Negado");
        }
    }
}