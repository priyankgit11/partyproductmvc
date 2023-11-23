using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PartyProductsMVC.Startup))]
namespace PartyProductsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
