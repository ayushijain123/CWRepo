using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FeedBack_180.Startup))]
namespace FeedBack_180
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
