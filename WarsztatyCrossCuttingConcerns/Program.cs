using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using WarsztatyCrossCuttingConcerns.Interceptors;

namespace WarsztatyCrossCuttingConcerns
{
    public class MojaKlasa
    {
        public virtual void MojaMetoda()
        {
            Console.WriteLine("Moja metoda");
        }
    }
    
    
    public class Program
    {
        public static void Main(string[] args)
        {
            // tutaj bedzie podpiecie "interceptora"
            var proxyGenerator = new ProxyGenerator();

            var obj = proxyGenerator
                .CreateClassProxy<MojaKlasa>(
                    new MyInterceptor()
                );

            //var mk = new MojaKlasa();
            obj.MojaMetoda();
            obj.MojaMetoda();
            
            
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .WriteTo.File(new CompactJsonFormatter(), "log.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseSerilog();
                });
    }
}
