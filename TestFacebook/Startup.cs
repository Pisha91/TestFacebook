using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestFacebook.Startup))]
namespace TestFacebook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
