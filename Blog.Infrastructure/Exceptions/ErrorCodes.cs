using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Exceptions
{
    public static class ErrorCodes
    {
        public static string EmailInUse => "email_in_use";
        public static string InvalidCredentials => "invalid_credentials";
        public static string UserNotFound => "user_not_found";
        public static string CategoryNotFound => "category_not_found";
        public static string PostNotFound => "post_not_found";
    }
}
