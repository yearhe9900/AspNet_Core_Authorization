using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AspNet_Core_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://*:8083")
                .UseStartup<Startup>()
                .Build();
    }
}
