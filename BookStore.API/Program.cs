using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API
{
    public class Program
    {
        private static IConfiguration config;

        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();

            var host = new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseUrls("http://localhost:5000", "http://localhost:5001", "http://localhost:5002")
               .UseIISIntegration()
               .UseStartup<Startup>()
               .Build();

            host.Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
