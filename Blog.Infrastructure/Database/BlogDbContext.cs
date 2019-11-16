using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Blog.Core.Domain;
using Blog.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Database
{
    public class BlogDbContext : DbContext
    {
        private readonly SqlServerSettings _settings;

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BlogDbContext(DbContextOptions options, SqlServerSettings settings)
            : base(options)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_settings.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase("blog-test-inMemory");
                return;
            }

            optionsBuilder.UseSqlServer(_settings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
