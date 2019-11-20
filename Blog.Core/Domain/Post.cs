using Blog.Common.Extensions;
using Blog.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public class Post : TimestampableEntityBase
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        protected Post() : base(Guid.NewGuid()) { }

        public Post(Guid id, string title, string content, Category category)
            : base(id)
        {
            SetTitle(title);
            SetContent(content);
            SetCategory(category);
        }

        public void SetTitle(string title)
        {
            if (title.Empty())
                throw new DomainException(ErrorCodes.InvalidPostTitle, "Title can not be empty.");
            if (title == Title)
                return;

            Title = title;
            UpdateModificationDate();
        }

        public void SetContent(string content)
        {
            if (content.Empty())
                throw new DomainException(ErrorCodes.InvalidPostContent, "Post content can not be empty.");
            if (content == Content)
                return;

            Content = content;
            UpdateModificationDate();
        }

        public void SetCategory(Category category)
        {
            if (category == null)
                throw new DomainException(ErrorCodes.InvalidPostCategory, "Post's category can not be null.");
            if (category.Id == CategoryId)
                return;

            Category = category;
            CategoryId = category.Id;
            UpdateModificationDate();
        }
    }
}
