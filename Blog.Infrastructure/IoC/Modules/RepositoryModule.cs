using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Blog.Infrastructure.Repositories;

namespace Blog.Infrastructure.IoC.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
