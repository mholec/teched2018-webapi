using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Teched2018.ApiModels;
using Teched2018.Repositories;

namespace Teched2018
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options => options.UseInMemoryDatabase("Teched2018"));

            var mvc = services.AddMvc(options =>
            {
                //options.InputFormatters.Add(new XmlSerializerInputFormatter());
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                //options.OutputFormatters.Insert(0,new XmlSerializerOutputFormatter());
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
                //options.ReturnHttpNotAcceptable = true;
            });

            mvc.AddJsonOptions(options =>
            {
                //if (options.SerializerSettings.ContractResolver != null)
                //{
                //    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                //    {
                //        NamingStrategy = new DefaultNamingStrategy(),
                //    };
                //}
            });

            services.AddCors(o =>
            {
                //o.AddPolicy("Default", cfg =>
                //{
                //    cfg.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                //});
            });

            services.AddResponseCaching();

            mvc.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = context =>
            //    {
            //        var problemDetails = new ValidationProblemDetails(context.ModelState)
            //        {
            //            Instance = context.HttpContext.Request.Path,
            //            Status = StatusCodes.Status400BadRequest,
            //            Type = "https://asp.net/core",
            //            Detail = "Please refer to the errors property for additional details."
            //        };

            //        return new BadRequestObjectResult(problemDetails)
            //        {
            //            ContentTypes = { "application/problem+json", "application/problem+xml" }
            //        };
            //    };
            //});

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "GunShop API", Version = "v1" });
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            app.UseResponseCaching();

            app.UseMvc();

            //app.UseCors("Default");

            //app.UseHttpCacheHeaders();

            app.UseDeveloperExceptionPage();

            //app.UseExceptionHandler(exc =>
            //      {
            //       exc.Run(async context =>
            //       {
            //        context.Response.StatusCode = 500;
            //        await context.Response.WriteAsync("Unexpected error happened");
            //       });
            //      });

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GunShop API V1");
            //});
        }
    }
}
