using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaCred9.Web.UI.Startup))]
namespace SistemaCred9.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
