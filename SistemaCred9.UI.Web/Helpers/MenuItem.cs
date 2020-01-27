namespace SistemaCred9.Web.UI.Helpers
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public object Parametros { get; set; }
        public bool Ativo { get; set; }

        public MenuItem(string text, string action, string controller, object parametros, bool ativo)
        {
            Text = text;
            Action = action;
            Controller = controller;
            Parametros = parametros;
            Ativo = ativo;
        }
    }
}