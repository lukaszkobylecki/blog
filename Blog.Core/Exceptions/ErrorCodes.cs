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
        public static string InvalidCategoryName => "invalid_category_name";
        public static string InvalidPostTitle => "invalid_post_title";
        public static string InvalidPostContent => "invalid_post_content";
        public static string InvalidPostCategory => "invalid_post_categoryId";
        public static string InvalidEntityName => "invalid_entity_name";
        public static string InvalidEntityId => "invalid_entity_id";
    }

}
