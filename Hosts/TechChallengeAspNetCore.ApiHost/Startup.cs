using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Eml.Mef;
using Eml.MefDependencyResolver.Api;
using Eml.ClassFactory.Contracts;
using TechChallengeAspNetCore.ApiHost.Configurations;
using TechChallengeAspNetCore.ApiHost.Helpers;
using Microsoft.AspNetCore.Rewrite;

namespace TechChallengeAspNetCore.ApiHost
{
    public class Startup
    {
        private const string SWAGGER_DOC = "v1";

        private const string API_NAME = "TechChallengeAspNetCore";

        private const string LAUNCH_URL = "docs";
       
		public static IConfiguration Configuration { get; private set; }

        public static ILoggerFactory LoggerFactory { get; private set; }
        
		public static IClassFactory ClassFactory { get; private set; }

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
                {
                    setupAction.ReturnHttpNotAcceptable = true;
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SWAGGER_DOC, new Info { Title = API_NAME, Version = "v1" });
                c.OperationFilter<SwashbuckleSummaryOperationFilter>();
                c.DocumentFilter<LowercaseDocumentFilter>();
            });
            ClassFactory = services.AddMef(() =>
            {
                // Register instances as shared.
                var instanceRegistrations = new List<Func<ContainerConfiguration, ExportDescriptorProvider>>
                {
                    r => r.WithInstance(LoggerFactory),
                    r => r.WithInstance(Configuration)
                };

                // Create Mef container
                return Bootstrapper.Init(API_NAME, instanceRegistrations);
            });

            //var rateLimits = ClassFactory.GetExport<RateLimitsConfig>();

            //services.AddMemoryCache();
            //services.Configure<IpRateLimitOptions>(options =>
            //{
            //    options.GeneralRules = rateLimits.Value;
            //});
            //services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            //services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            //services.AddHttpCacheHeaders(
            //    expirationModelOptions =>
            //    {
            //        expirationModelOptions.MaxAge = 600;
            //        expirationModelOptions.SharedMaxAge = 300;
            //    },
            //    validationModelOptions =>
            //    {
            //        validationModelOptions.AddMustRevalidate = true;
            //        validationModelOptions.AddProxyRevalidate = true;
            //    });
            //services.AddResponseCaching();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            LoggerFactory.AddConsole();
            LoggerFactory.AddDebug(LogLevel.Information);
            LoggerFactory.AddNLog();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = 500;

                    if (exceptionHandlerFeature != null)
                    {
                        var logger = LoggerFactory.CreateLogger("Global exception logger");
                        logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);

                        #if DEBUG
                            await context.Response.WriteAsync(exceptionHandlerFeature?.Error.Message);
                        #else
						    await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                        #endif
                    }
                    else
                    {
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    }
                });
            });
            //app.UseIpRateLimiting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{SWAGGER_DOC}/swagger.json", API_NAME);
                c.RoutePrefix = LAUNCH_URL;
                c.EnableFilter();
            });

            var whiteListConfig = new WhiteListConfig(Configuration);

            app.UseCors(builder => builder.WithOrigins(whiteListConfig.Value.ToArray())
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            //app.UseResponseCaching();
            //app.UseHttpCacheHeaders(); 
            app.UseMvc();
        }
    }
}

