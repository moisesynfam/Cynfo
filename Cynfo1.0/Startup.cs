using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cynfo1._0.Startup))]
namespace Cynfo1._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
