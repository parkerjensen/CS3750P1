using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CS3750P1.Startup))]
namespace CS3750P1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
