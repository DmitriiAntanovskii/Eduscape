using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OEG.Startup))]
namespace OEG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
