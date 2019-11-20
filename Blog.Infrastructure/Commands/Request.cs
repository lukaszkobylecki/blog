using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands
{
    public class Request
    {
        public Guid Id { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }

        public static Request Create(Guid id, string host, string path, string method)
            => new Request
            {
                Id = id,
                Host = host,
                Path = path,
                Method = method
            };
    }
}
