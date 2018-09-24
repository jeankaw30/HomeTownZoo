using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeTownZoo.Startup))]
namespace HomeTownZoo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
