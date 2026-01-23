namespace RaspiRobot;

using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using RaspiRobot.Logging;
using RaspiRobot.OpenApi;
using RaspiRobot.Web.Features.Devices.Robot.LiveUpdate;
using RaspiRobot.Web.Features.OperationMode.LiveUpdate;
using Serilog;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc(o => o.Interceptors.Add<LoggingInterceptor>());
        services.AddSignalR();

        services
            .AddControllers()
            .AddJsonOptions(o => { o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.CustomSchemaIds(type => type.ToString());
            c.SwaggerDoc("web", new OpenApiInfo { Title = "RaspiRobot WEB" });
            c.ResolveConflictingActions(x => x.First());
        });

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseForwardedHeaders();
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseForwardedHeaders();
            app.UseHsts();
        }

        app.UseSerilogRequestLogging();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseSwagger(o =>
        {
            o.RouteTemplate = "swagger/{documentName}/swagger.json";
            o.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
        });
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/web/swagger.json", "RaspiRobot WEB");
            c.RoutePrefix = "swagger";
            c.DisplayRequestDuration();
        });

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseMiddleware<RequestLoggingMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            GrpcServiceMapper.MapGrpcServices(endpoints);

            endpoints.MapHub<RobotAxisPositionChangedHub>($"/{nameof(RobotAxisPositionChangedHub)}");
            endpoints.MapHub<OperationModeChangedHub>($"/{nameof(OperationModeChangedHub)}");

            endpoints.MapControllers();
        });
    }
}