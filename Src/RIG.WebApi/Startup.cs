using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RIG.AccountModule.Infrastructure;
using RIG.Shared.Infrastructure;
using RIG.WebApi.Controllers;
using RIG.WebApi.Middleware;

namespace RIG.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                                       {
                                           options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                                           options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                                           options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.Default;
                                           options.SerializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;
                                           options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                                           options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                                           options.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
                                           options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
                                       })
                    .AddApplicationPart(typeof(HomeController).Assembly);

            services.AddSwaggerGen(c =>
                                   {
                                       c.SwaggerDoc("v1", new OpenApiInfo());

                                       c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                                                         {
                                                                             Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                                                                             Name = "Authorization",
                                                                             In = ParameterLocation.Header,
                                                                             Type = SecuritySchemeType.ApiKey
                                                                         });
                                       c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                {
                                                                    {
                                                                        new OpenApiSecurityScheme
                                                                        {
                                                                            Reference = new OpenApiReference
                                                                                        {
                                                                                            Type = ReferenceType.SecurityScheme,
                                                                                            Id = "Bearer"
                                                                                        },
                                                                        },
                                                                        new List<string>()
                                                                    }
                                                                });

                                       c.CustomSchemaIds(type => type.ToString());
                                   });


            CompositionRootRegisterer compositionRootRegisterer = new CompositionRootRegisterer(services, _configuration);
            compositionRootRegisterer.Registerer(new SharedCompositionRoot())
                                     .Registerer(new AccountModuleCompositionRoot());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<GeneralExceptionHandlerMiddleware>();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ""); });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(builder => builder.MapControllers());
        }
    }
}