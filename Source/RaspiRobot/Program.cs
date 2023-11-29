using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RaspiRobot;

using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DocumentStorage.FileBased;
using RaspiRobot.BackgroundServices;
using RaspiRobot.Common;
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
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {CorrelationId} {Level:u3}] {Username} {Message:lj}{NewLine}{Exception}",
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
                .UseKestrel()
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .UseWebRoot(contentDirectory));
    }

    private static void RegisterModules(ContainerBuilder builder)
    {
        builder.RegisterModule<CommonModule>();
        builder.RegisterModule<DocumentStorageFileBasedModule>();
        builder.RegisterModule<RobotControlGrabItModule>();
        builder.RegisterModule<RobotControlModule>();
    }
}