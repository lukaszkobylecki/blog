using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Category
{
    public class DeleteCategory : ICommand
    {
        public int Id { get; set; }
    }
}
