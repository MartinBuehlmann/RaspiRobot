namespace RaspiRobot;

using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DocumentStorage.FileBased;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaspiRobot.BackgroundServices;
using RaspiRobot.Common;
using RaspiRobot.OpenApi;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.GrabIt;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
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
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(Startup).Assembly.GetName().Name)
                .UseStartup<Startup>()
                .UseWebRoot(contentDirectory));
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