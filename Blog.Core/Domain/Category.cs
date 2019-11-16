using Blog.Common.Extensions;
using Blog.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        protected Category() { }

        public Category(string name)
        {
            SetName(name);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (name.Empty())
                throw new DomainException(ErrorCodes.InvalidCategoryName, "Category name can not be empty.");

            Name = name;
            UpdateModificationDate();
        }

        private void UpdateModificationDate()
            => UpdatedAt = DateTime.UtcNow;
    }
}
