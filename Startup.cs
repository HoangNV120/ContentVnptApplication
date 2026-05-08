using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContentVnptApplication.Startup))]
namespace ContentVnptApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
