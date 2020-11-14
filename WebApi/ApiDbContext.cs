using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) :
            base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new BlogMap(builder.Entity<Blog>());
        }
    }

    public class BlogMap
    {
        public BlogMap(EntityTypeBuilder<Blog> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("blog");
            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.Title).HasColumnName("title");
            entityBuilder.Property(x => x.Description).HasColumnName("description");
        }
    }
}
