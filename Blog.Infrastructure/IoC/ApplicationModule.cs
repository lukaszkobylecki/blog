using Autofac;
using Microsoft.Extensions.Configuration;
using Blog.Infrastructure.IoC.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.IoC
{
    public class ApplicationModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ApplicationModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new SettingsModule(_configuration));
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<EventModule>();
            builder.RegisterModule<MapperModule>();
            builder.RegisterModule<ServiceModule>();
        }
    }
}
