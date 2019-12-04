using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events.User
{
    public class UserCreated : IEvent
    {
        public UserDto User { get; set; }

        public UserCreated(UserDto user)
        {
            User = user;
        }
    }
}
