using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.DTO
{
    public class UserDto : DateTrackingEntityDtoBase
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
