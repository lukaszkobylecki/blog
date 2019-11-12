using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Settings
{
    public class SqlServerSettings
    {
        public string ConnectionString { get; set; }
        public bool InMemory { get; set; }
    }
}
