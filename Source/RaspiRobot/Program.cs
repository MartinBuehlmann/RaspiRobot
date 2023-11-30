namespace RaspiRobot;

using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DocumentStorage.FileBased;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaspiRobot.BackgroundServices;
using RaspiRobot.Common;
using RaspiRobot.OpenApi;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.GrabIt;
using Serilog;
using Serilog.Events;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                "./../logs/RaspiRobot-.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate:
                "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {CorrelationId} {Level:u3}] {Username} {Message:lj}{NewLine}{Exception}",
                fileSizeLimitBytes: 10 * 1024 * 1024,
                retainedFileCountLimit: 10,
                rollOnFileSizeLimit: true,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1))
            .CreateLogger();

        string? currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Log.Information("Setting the current directory to '{CurrentDirectory}'", currentDirectory);
        Directory.SetCurrentDirectory(currentDirectory!);
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        string contentDirectory = $"{DirectoryProvider.ResolveContentDirectory()}/wwwroot";
        Log.Information("Setting the web root directory to '{ContentDirectory}'", contentDirectory);

        return Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(RegisterModules)
            .UseSerilog()
            .ConfigureServices((_, services) => services.AddHostedService<BackgroundServiceHost>())
            .ConfigureWebHostDefaults(webBuilder => webBuilder
                .UseStartup<Startup>()
                .UseKestrel(
                    kestrel =>
                    {
                        HostingConfiguration hostingConfiguration = RetrieveHostingConfiguration(kestrel);
                        kestrel.Listen(
                            IPAddress.Any,
                            hostingConfiguration.Port,
                            listenOptions =>
                            {
                                if (hostingConfiguration.UseHttps)
                                {
                                    string executingAssemblyDirectory =
                                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) !;
                                    string certificateFilePath = Path.Combine(executingAssemblyDirectory!,
                                        hostingConfiguration.CertificateFile!);
                                    listenOptions.UseHttps(new X509Certificate2(certificateFilePath,
                                        hostingConfiguration.CertificatePassword));
                                }
                            });
                    })
                .UseWebRoot(contentDirectory));
    }

    private static HostingConfiguration RetrieveHostingConfiguration(KestrelServerOptions kestrel)
    {
        IConfigurationSection endpointConfiguration = kestrel.ApplicationServices
            .GetService<IConfiguration>() !
            .GetSection("Kestrel")
            .GetSection("EndpointDefaults");
        IConfigurationSection certificateConfiguration = endpointConfiguration.GetSection("CertificateSettings");

        return new HostingConfiguration(
            endpointConfiguration.GetValue<bool>("UseHttps"),
            endpointConfiguration.GetValue<int>("Port"),
            certificateConfiguration.GetValue<string>("FileName"),
            certificateConfiguration.GetValue<string>("Password"));
    }

    private static void RegisterModules(ContainerBuilder builder)
    {
        builder.RegisterModule<CommonModule>();
        builder.RegisterModule<DocumentStorageFileBasedModule>();
        builder.RegisterModule<OpenApiModule>();
        builder.RegisterModule<RobotControlGrabItModule>();
        builder.RegisterModule<RobotControlModule>();
    }
}