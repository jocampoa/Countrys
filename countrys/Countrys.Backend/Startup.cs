using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Countrys.Backend.Startup))]
namespace Countrys.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
