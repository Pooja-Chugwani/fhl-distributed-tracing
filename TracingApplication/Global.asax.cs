using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Exporter.Geneva;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace TracingApplication
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private TracerProvider tracerProvider;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddAspNetInstrumentation()
            .AddSource("TracingApplication")
            .SetSampler(new AlwaysOnSampler())
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService("TracingApplication"))
            
            .AddZipkinExporter()
            .AddGenevaTraceExporter(o =>
            {
                o.ConnectionString = "EtwSession=OpenTelemetry";
            })
            /*.AddOtlpExporter(options =>
            {
                options.Endpoint =
                    new Uri("http://localhost:4317");
            })*/
            .Build();

            /*var host = Host.CreateDefaultBuilder()
                .ConfigureServices(this.ConfigureServices)
                .Build();
            _ = host.Services.GetService<IHostedService>();*/
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            /*serviceCollection
                .AddOpenTelemetryTracing((builder) => builder
                    .AddSource("SampleService")
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault().AddService("SampleService", serviceVersion: "1.0.0")
                    )
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddConsoleExporter()
                    //.AddZipkinExporter()
               );*/
        }
    }
}
