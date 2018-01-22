using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryTrackingSystem.Startup))]
namespace InventoryTrackingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
