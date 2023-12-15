namespace RaspiRobot.Common.Logging;

using System;
using Serilog;
using Serilog.Core;

public class Log
{
    private readonly ILogger log;

    public Log()
    {
        this.log = Serilog.Log.Logger;
    }

    [MessageTemplateFormatMethod("message")]
    public void Fatal(Exception exception, string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Fatal(exception, message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Error(string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Error(message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Error(Exception exception, string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Error(exception, message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Warn(string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Warning(message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Warn(Exception exception, string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Warning(exception, message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Info(string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Information(message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Info(Exception exception, string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Information(exception, message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Debug(string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Debug(message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Debug(Exception exception, string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Debug(exception, message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Verbose(string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Verbose(message, parameters);
    }

    [MessageTemplateFormatMethod("message")]
    public void Verbose(Exception exception, string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this.log.Verbose(exception, message, parameters);
    }
}
