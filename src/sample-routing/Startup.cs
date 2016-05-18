using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace sample_routing
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(minLevel: LogLevel.Trace);

            var routeBuilder = new RouteBuilder(app);


            routeBuilder.MapRoute("site/category/{item}", c =>
            {
                var item = c.GetRouteValue("item");
                return c.Response.WriteAsync($"Your request for {item} is just fine.");
            });

            routeBuilder.MapRoute(string.Empty, c => c.Response.WriteAsync("Hello, World!"));
            app.UseRouter(routeBuilder.Build());
        }
    }
}
