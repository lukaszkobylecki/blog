using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Handlers;
using Blog.Infrastructure.Query.Handlers.Category;
using Blog.Infrastructure.Query.Queries;
using Blog.Infrastructure.Query.Queries.Category;

namespace Blog.Infrastructure.IoC.Modules
{
    public class QueryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(QueryModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .InstancePerLifetimeScope();

            builder.RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}
