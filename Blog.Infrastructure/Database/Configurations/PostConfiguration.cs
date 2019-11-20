using Blog.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Database.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(false);
            builder.Property(x => x.ClusterId).UseIdentityColumn();
            builder.HasIndex(x => x.ClusterId).IsClustered();
        }
    }
}
