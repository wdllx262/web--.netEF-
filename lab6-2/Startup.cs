using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab6_2.Startup))]
namespace lab6_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
