﻿using Autofac;
using Autofac.Builder;

namespace EventBroker.Autofac;

using System;

public static class EventBrokerRegistrationExtensions
{
    public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle>
        RegisterOnEventBroker<TLimit, TActivatorData, TSingleRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration)
        where TSingleRegistrationStyle : SingleRegistrationStyle
        => registration
            .OnActivated(
                x =>
                {
                    var subscription = x.Context.Resolve<IEventRegistration>();
                    if (x.Instance == null)
                    {
                        throw new InvalidOperationException("Instance is null");
                    }

                    subscription.Register((IEventSubscriptionBase)x.Instance);
                });
}