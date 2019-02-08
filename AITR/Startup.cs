using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AITR.Startup))]
namespace AITR
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
