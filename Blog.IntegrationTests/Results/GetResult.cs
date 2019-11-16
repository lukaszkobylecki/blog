using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Blog.IntegrationTests.Results
{
    public class GetResult<T>
    {
        public HttpResponseMessage Response { get; set; }
        public T Data { get; set; }
    }
}
