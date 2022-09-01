using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.OData.UriParser;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

using Microsoft.EntityFrameworkCore;
using API.Protocol.OData;
using API.Protocol.OData.Controller;
using Api.Query;

//using Microsoft.Extensions.DependencyInjection; // IServiceCollection

//using Microsoft.AspNet.OData.Interfaces;

//using Microsoft.OData;

namespace Api
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
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=true;MultipleActiveResultsets=true;";
            string routePrefix = "feed";

            IEnumerable<Type> controllerTypes = typeof(Api.Startup).Assembly.GetTypes()
                .Where(d =>
                typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(d.BaseType) && d.Namespace.Split(".").LastOrDefault() == "Controller");

            //services.AddSingleton<IControllerDomain, ControllerDomain>((provider) => new ControllerDomain(controllerTypes));

            //services.AddSingleton<IODataControllerActivator, ODataControllerActivator>();

           // services.AddSingleton<IQueryableBuilder, QueryableBuilder>();

            services.AddHttpContextAccessor().AddControllers()
            .AddOData((opt, provider) =>
            {
                opt.AddRouteComponents(
                    routePrefix, 
                    Model.ModelBuilder.GetEdmModel(typeof(Packt.Shared.NorthwindContext)),
                    routeServices => routeServices
                    .AddSingleton<IQueryableBuilder, QueryableBuilder>()
                    .AddDbContext<DbContext,Packt.Shared.NorthwindContext>(options => options.UseSqlServer(connectionString).UseLoggerFactory(new Packt.Shared.ConsoleLoggerFactory()))
                    //.AddSingleton<IQueryableBuilder, QueryableBuilder>()
                    )
                .Count().Filter().Expand().Select().OrderBy().SetMaxTop(100)
                .Conventions.Add(new ODataControllerActivator(new ControllerDomain(controllerTypes)) /*provider.GetRequiredService<IODataControllerActivator>()*/);

               // opt.RouteComponents[routePrefix].ServiceProvider.GetService(typeof(IQueryableBuilder));

            });

            //services.AddDbContext<DbContext, Packt.Shared.NorthwindContext>(options =>
            //  options.UseSqlServer(connectionString)
            //  .UseLoggerFactory(new Packt.Shared.ConsoleLoggerFactory()));

            //services.AddDbContext<Packt.Shared.NorthwindContext>(options =>
            //  options.UseSqlServer(connectionString)
            //  .UseLoggerFactory(new Packt.Shared.ConsoleLoggerFactory()));

            // services.AddDbContext<Packt.Shared.NorthwindContext>();
        }

        private IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<Packt.Shared.Category>("Categories");
            builder.EntitySet<Packt.Shared.Product>("Products");
            builder.EntitySet<Packt.Shared.Supplier>("Suppliers");
            return builder.GetEdmModel();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/api");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
