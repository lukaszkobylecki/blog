using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Microsoft.Extensions.Caching.Memory;
using Blog.Infrastructure.Services;

namespace Blog.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .SingleInstance();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .SingleInstance();

            builder.RegisterType<MemoryCache>()
                .As<IMemoryCache>()
                .InstancePerLifetimeScope();
        }
    }
}
