using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiveMusicLovers.Web.UI.Startup))]
namespace LiveMusicLovers.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
