using Blog.Common.Extensions;
using Blog.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public class Category : DateTrackingEntityBase
    {
        public string Name { get; private set; }

        protected Category() { }

        public Category(string name) 
            : base()
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            if (name.Empty())
                throw new DomainException(ErrorCodes.InvalidCategoryName, "Category name can not be empty.");
            if (name == Name)
                return;

            Name = name;
            UpdateModificationDate();
        }
    }
}
