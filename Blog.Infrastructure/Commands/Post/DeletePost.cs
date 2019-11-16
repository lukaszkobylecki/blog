using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Post
{
    public class DeletePost : ICommand
    {
        public int Id { get; set; }
    }
}
