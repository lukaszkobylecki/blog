using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Event.Events.User
{
    public class UserDeleted : IEvent
    {
        public Guid Id { get; set; }

        public UserDeleted(Guid id)
        {
            Id = id;
        }
    }
}
