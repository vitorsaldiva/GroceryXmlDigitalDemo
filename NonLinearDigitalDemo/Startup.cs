using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NonLinearDigitalDemo.Startup))]
namespace NonLinearDigitalDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
