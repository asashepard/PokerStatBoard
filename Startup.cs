using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PokerStatBoard.Startup))]
namespace PokerStatBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
