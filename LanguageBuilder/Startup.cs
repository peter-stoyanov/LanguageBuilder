using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LanguageBuilder.Startup))]
namespace LanguageBuilder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
