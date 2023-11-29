using System;
using System.IO;
using System.Linq;

namespace RaspiRobot;

using System.Diagnostics;
using System.Reflection;

internal static class DirectoryProvider
{
    private static bool IsDebugBuild { get; set; }

    public static string ResolveContentDirectory()
    {
        CheckForDebugBuild();

        if (IsDebugBuild)
        {
            return SearchWebDirectory(Directory.GetCurrentDirectory());
        }

        return ResolveSettingsDirectory();
    }

    private static string ResolveSettingsDirectory()
    {
        string? location = Assembly.GetEntryAssembly()?.Location;
        string? contentDirectory = Path.GetDirectoryName(location);
        if (!string.IsNullOrEmpty(contentDirectory) && Directory.GetDirectories(contentDirectory).Any(x => x.EndsWith("wwwroot", StringComparison.InvariantCulture)))
        {
            return contentDirectory;
        }

        return Directory.GetCurrentDirectory();
    }

    private static string SearchWebDirectory(string currentDirectory)
    {
        string directory = currentDirectory;

        while (true)
        {
            string[] directories = Directory.GetDirectories(directory);
            string? webDir = directories.SingleOrDefault(x => x.EndsWith("RaspiRobot.Web", StringComparison.InvariantCulture));
            if (!string.IsNullOrEmpty(webDir))
            {
                return webDir;
            }

            directory = Directory.GetParent(directory)?.FullName!;
        }
    }

    [Conditional("DEBUG")]
    private static void CheckForDebugBuild()
    {
        IsDebugBuild = true;
    }
}