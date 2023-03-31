using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PokeBindaWebsite.Startup))]
namespace PokeBindaWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
