using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Settings
{
    public class EncryptionSettings
    {
        public int DeriveBytesIterationsCount { get; set; }
        public int SaltSize { get; set; }
    }
}
