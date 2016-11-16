using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Garage_2._1.Startup))]
namespace Garage_2._1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
