using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RulesService.Presentation.Api
{
    public static class Program
    {
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
    }
}