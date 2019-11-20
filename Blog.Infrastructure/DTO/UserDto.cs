using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.DTO
{
    public class UserDto : TimestampableEntityDtoBase
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
