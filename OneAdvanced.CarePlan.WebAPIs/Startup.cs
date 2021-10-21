using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OneAdvanced.CarePlan.WebAPIs.Application;
using OneAdvanced.CarePlan.WebAPIs.Application.Infrastructures;
using OneAdvanced.CarePlan.WebAPIs.Application.Models.Json;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils.Swagger;

namespace OneAdvanced.CarePlan.WebAPIs
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                //.AddJsonOptions(options =>
                //{
                //    options.JsonSerializerOptions.Converters.Add(new CarePlanJsonConverter());
                //})
                .AddNewtonsoftJson(options =>
                {
                    //options.SerializerSettings.Converters.Add(new CarePlanJsonConverter());
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Advanced Care Plan Exercise WebAPIs", Version = "1.0.0" });
                c.IncludeXmlComments(Path.Combine(Environment.ContentRootPath, "OneAdvanced.CarePlan.WebAPIs.xml"));
                c.ParameterFilter<CustomSwaggerParameterFilter>();
                c.SchemaFilter<CustomSwaggerSchemaFilter>();
                c.DocumentFilter<CustomSwaggerDocumentFilter>();
                c.OperationFilter<CustomSwaggerOperationFilter>();
                c.RequestBodyFilter<CustomSwaggerBodyFilter>();
            });
            services.AddCors();
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddDbContext<CarePlanDbContext>(options => options.UseInMemoryDatabase("Db"));
            services.AddScoped<CarePlanService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseExceptionHandler("/Error");
            app.ConfigureExceptionHandler();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnAdvanced.CreateCarePlanModel.WebAPIs v1"));

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
