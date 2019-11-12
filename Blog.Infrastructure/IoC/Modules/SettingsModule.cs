using Autofac;
using Microsoft.Extensions.Configuration;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<SqlServerSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<EncryptionSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<JwtSettings>())
                .SingleInstance();
        }
    }
}
