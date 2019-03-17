using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LeadTheWay.Areas.Identity.IdentityHostingStartup))]
namespace LeadTheWay.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}