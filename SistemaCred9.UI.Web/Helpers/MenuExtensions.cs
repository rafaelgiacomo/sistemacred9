using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;

namespace SistemaCred9.Web.UI.Helpers
{
    public static class MenuExtensions
    {
        //MenuItem Sem Parametro
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper, string text, string action, string controller)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, controller).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        //MenuItem com parametro
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper, string text, string action,
            string controller, object list, bool ativo)
        {
            var li = new TagBuilder("li");
            if (ativo)
            {
                li.AddCssClass("active");
            }

            li.InnerHtml = htmlHelper.ActionLink(text, action, controller, list, null).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuDropDown(this HtmlHelper htmlHelper, string text, string[] texts, string[] actions,
            string[] controllers, object[] values)
        {
            var li = new TagBuilder("li");
            var ul = new TagBuilder("ul");

            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            StringBuilder innerHtmlLi = new StringBuilder();
            StringBuilder innerHtmlUl = new StringBuilder();

            //Adicionando a classe CSS do item e da lista do submenu
            li.AddCssClass("treeview");
            ul.AddCssClass("treeview-menu");

            //Adicionando os Itens do submenu
            for (int i = 0; i < texts.Length; i++)
            {
                innerHtmlUl.Append(MenuItem(htmlHelper, texts[i], actions[i], controllers[i], values[i], false));
            }
            ul.InnerHtml = innerHtmlUl.ToString();

            innerHtmlLi.Append(MenuItemDropDown(text));
            innerHtmlLi.Append(ul);

            //Verificando se um dos itens do submenu está ativado
            for (int i = 0; i < texts.Length; i++)
            {
                if (string.Equals(currentController, controllers[i], 
                    StringComparison.OrdinalIgnoreCase))
                {
                    li.AddCssClass("active");
                }
            }

            li.InnerHtml = innerHtmlLi.ToString();
            return MvcHtmlString.Create(li.ToString());
        }

        //Método que cria o item do menu principal que irá conter um submenu
        public static MvcHtmlString MenuItemDropDown(string text)
        {
            var span = new TagBuilder("span");
            var a = new TagBuilder("a");
            var i = new TagBuilder("i");
            StringBuilder innerHtmlA = new StringBuilder();

            a.MergeAttribute("href", "#");
            i.AddCssClass("fa fa-angle-left pull-right");
            span.InnerHtml = text;

            innerHtmlA.Append(span);
            innerHtmlA.Append(i);

            a.InnerHtml = innerHtmlA.ToString();
            return MvcHtmlString.Create(a.ToString());
        }
    }
}