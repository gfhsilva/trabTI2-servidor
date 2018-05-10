using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(trabalhoTi2Final.Startup))]
namespace trabalhoTi2Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
