using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PokemonPicker.Startup))]
namespace PokemonPicker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
