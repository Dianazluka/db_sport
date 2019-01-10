using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sport.Startup))]
namespace sport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
