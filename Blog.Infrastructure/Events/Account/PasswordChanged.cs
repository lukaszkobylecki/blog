﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events.Account
{
    public class PasswordChanged : IEvent
    {
        public Guid UserId { get; set; }

        public PasswordChanged(Guid userId)
        {
            UserId = userId;
        }
    }
}
