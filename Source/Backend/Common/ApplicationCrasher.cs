namespace Common;

using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Serilog;

public class ApplicationCrasher
{
    public void CrashApplication(Exception exception)
    {
        Log.Fatal(exception, "Crash application due of an unhandled exception");
        var thread = new Thread(() => ExceptionDispatchInfo.Capture(exception).Throw())
        {
            Name = nameof(ApplicationCrasher),
        };
        thread.Start();
        thread.Join();
    }
}