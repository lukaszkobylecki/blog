using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Blog.Infrastructure.Event.Handlers;
using Blog.Infrastructure.Event.Events;

namespace Blog.Infrastructure.IoC.Modules
{
    public class EventModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(EventModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<EventPublisher>()
                .As<IEventPublisher>()
                .InstancePerLifetimeScope();
        }
    }
}
