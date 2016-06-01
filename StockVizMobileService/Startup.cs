using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(StockVizMobileService.Startup))]

namespace StockVizMobileService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}