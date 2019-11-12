using Blog.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Exceptions
{
    public class ServiceException : BlogException
    {
        public ServiceException()
        {
        }

        public ServiceException(string code) 
            : base(code)
        {
        }

        public ServiceException(string message, params object[] args) 
            : base(string.Empty, message, args)
        {
        }

        public ServiceException(string code, string message, params object[] args) 
            : base(code, message, args)
        {
        }

        public ServiceException(Exception innerException, string message, params object[] args)
            : base(innerException, string.Empty, message, args)
        {
        }

        public ServiceException(Exception innerException, string code, string message, params object[] args)
            : base(innerException, code, string.Format(message, args), args)
        {
        }
    }
}
