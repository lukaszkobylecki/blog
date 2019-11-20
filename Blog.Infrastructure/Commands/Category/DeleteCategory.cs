﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Category
{
    public class DeleteCategory : IResourceCommand
    {
        public Request Request { get; set; }
        public Guid ResourceId { get; set; }
    }
}
