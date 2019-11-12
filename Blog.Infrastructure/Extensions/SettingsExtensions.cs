using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Extensions
{
    public static class SettingsExtensions
    {
        public static T GetSettings<T>(this IConfiguration configuration) where T : new()
        {
            var configurationObject = new T();
            var section = typeof(T).Name.Replace("Settings", "");

            configuration.GetSection(section).Bind(configurationObject);

            return configurationObject;
        }
    }
}
