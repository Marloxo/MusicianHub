using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicianHub.Startup))]
namespace MusicianHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
