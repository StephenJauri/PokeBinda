using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PokeBindaOnline.Startup))]
namespace PokeBindaOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
