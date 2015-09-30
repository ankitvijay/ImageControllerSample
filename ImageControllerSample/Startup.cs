using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ImageControllerSample.Startup))]

namespace ImageControllerSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
