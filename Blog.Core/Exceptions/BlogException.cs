using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Exceptions
{
    public abstract class BlogException : Exception
    {
        public string Code { get; protected set; }

        protected BlogException() { }

        protected BlogException(string code)
        {
            Code = code;
        }

        protected BlogException(string message, params object[] args) 
            : this(string.Empty, message, args) { }

        protected BlogException(string code, string message, params object[] args) 
            : this(null, code, message, args) { }

        protected BlogException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args){ }

        protected BlogException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
