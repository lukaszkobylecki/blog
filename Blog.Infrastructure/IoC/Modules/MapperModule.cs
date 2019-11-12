using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Blog.Infrastructure.Mappers;

namespace Blog.Infrastructure.IoC.Modules
{
    public class MapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                .SingleInstance();
        }
    }
}
