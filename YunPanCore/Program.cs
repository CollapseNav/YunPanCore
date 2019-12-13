using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace YunPanCore
{
    public class Program {
        public static void Main (string[] args) {
            CreateWebHostBuilder (args).Build ().Run ();
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseKestrel ()
            .UseContentRoot (Directory.GetCurrentDirectory ())
            .UseUrls ("http://*:5000;https://*:5001")
            .UseIISIntegration ()
            .UseStartup<Startup> ();
    }
}