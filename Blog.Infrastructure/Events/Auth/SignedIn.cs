using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events.Auth
{
    public class SignedIn : IEvent
    {
        public UserDto User { get; set; }

        public SignedIn(UserDto user)
        {
            User = user;
        }
    }
}
