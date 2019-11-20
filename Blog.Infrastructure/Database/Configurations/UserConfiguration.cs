using Blog.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(false);
            builder.Property(x => x.ClusterId).UseIdentityColumn();
            builder.HasIndex(x => x.ClusterId).IsClustered();
        }
    }
}
