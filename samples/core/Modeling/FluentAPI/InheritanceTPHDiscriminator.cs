﻿using Microsoft.EntityFrameworkCore;

namespace EFModeling.FluentAPI.InheritanceTphDiscriminator
{
    #region Inheritance
    class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasDiscriminator<string>("blog_type")
                .HasValue<Blog>("blog_base")
                .HasValue<RssBlog>("blog_rss");
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
    }

    public class RssBlog : Blog
    {
        public string RssUrl { get; set; }
    }
    #endregion
}
