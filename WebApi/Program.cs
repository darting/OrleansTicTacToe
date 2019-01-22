using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Serilog;
using System;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Verbose()
                                .WriteTo.File("log.txt")
                                .WriteTo.Console()
                                .CreateLogger();

            var client = new ClientBuilder()
                                .UseLocalhostClustering()
                                .Configure<ClusterOptions>(x =>
                                {
                                    x.ClusterId = "dev";
                                    x.ServiceId = "helloworld";
                                })
                                .ConfigureLogging(l =>
                                {
                                    l.AddSerilog(dispose: true);
                                })
                                .Build();
            await client.Connect(async ex =>
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                return true;
            });


            CreateWebHostBuilder(args, client).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IClusterClient client) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton(client);
                })
                .ConfigureLogging(x => x.AddSerilog())
                .UseStartup<Startup>();
    }
}
