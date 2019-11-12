using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Exceptions
{
    public static class ErrorCodes
    {
        public static string InvalidEmail => "invalid_email";
        public static string InvalidPassword => "invalid_password";
        public static string InvalidUsername => "invalid_username";
    }
}
