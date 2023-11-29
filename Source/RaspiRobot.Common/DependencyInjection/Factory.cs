namespace RaspiRobot.Common.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

public class Factory
{
    private readonly IComponentContext componentContext;

    public Factory(IComponentContext componentContext)
    {
        this.componentContext = componentContext;
    }

    protected Factory()
        : this(null!)
    {
    }

    public virtual T Create<T>(params object[] parameters)
        where T : notnull
    {
        List<TypedParameter> parameterList = CreateParameters(parameters);
        return this.componentContext.Resolve<T>(parameterList);
    }

    private static List<TypedParameter> CreateParameters(object[] parameters)
    {
        IEnumerable<TypedParameter> typedParameters = parameters
            .SelectMany(x => x
                .GetType()
                .GetInterfaces()
                .Where(y =>
                    y != typeof(IDisposable) &&
                    y.Namespace?.StartsWith("System") is false)
                .Append(x.GetType())
                .Select(y => new TypedParameter(y, x)))
            .ToList();
        IEnumerable<TypedParameter> baseTypes = parameters
            .Where(x =>
                x.GetType().GetTypeInfo().BaseType != null &&
                x.GetType().GetTypeInfo().BaseType != typeof(object))
            .Select(x => new TypedParameter(x.GetType().GetTypeInfo().BaseType!, x));
        return typedParameters.Concat(baseTypes).ToList();
    }
}