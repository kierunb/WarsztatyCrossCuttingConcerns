using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core.Configurations;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WarsztatyCrossCuttingConcerns.ActionFilters;

namespace WarsztatyCrossCuttingConcerns
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddElmah();

            services.AddScoped<MyActionFilter>();

            services.AddEasyCaching(options =>
            {
                //use memory cache that named default
                options.UseInMemory("default");

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            

            if (env.IsDevelopment())
            {

                //app.UseDeveloperExceptionPage();
            }
            else
            { }
                // na produkcji lapiemy wszystkie nieobsluzone wyjatki
                // logujemy je
                // i wyswietlamy userowi "ladna" strone z generycznym opiem bledu

                // correlation ID
                //Guid correlationId = Guid.NewGuid();

                //app.UseExceptionHandler(errorApp =>
                //{
                //    errorApp.Run(async context =>
                //    {
                //        var exceptionHandlerPathFeature =
                //            context.Features.Get<IExceptionHandlerPathFeature>();

                //        logger.LogError($"CorrelationId: {correlationId}, Exception: ", exceptionHandlerPathFeature?.Error);

                //        context.Response.StatusCode = 500;
                //        context.Response.ContentType = "text/html";

                //        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                //        await context.Response.WriteAsync("ERROR!<br><br>\r\n");
                //        await context.Response.WriteAsync($"CorrelationID: {correlationId}");

                //        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                //        await context.Response.WriteAsync("</body></html>\r\n");
                //    });
                //});


            app.UseElmah();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
