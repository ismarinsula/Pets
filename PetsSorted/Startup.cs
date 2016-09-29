using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetsSorted.Startup))]
namespace PetsSorted
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
