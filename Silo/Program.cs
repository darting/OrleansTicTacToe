using GrainInterfaces;
using Grains;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Silo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();
                await host.StopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .UseDashboard(x =>
                {

                })
                .Configure<ClusterOptions>(x =>
                {
                    x.ClusterId = "dev";
                    x.ServiceId = "helloworld";
                })
                .Configure<EndpointOptions>(x => x.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(GameGrain).Assembly).WithCodeGeneration()
                         .AddApplicationPart(typeof(IGame).Assembly).WithCodeGeneration()
                         .AddApplicationPart(typeof(TicTacToe.Game).Assembly).WithCodeGeneration();
                })
                .ConfigureLogging(l => l.AddConsole());

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
