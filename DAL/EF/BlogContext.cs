using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL.EF
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasConversion(x => x.ToString(),
                        x => (Role)Enum.Parse(typeof(Role), x));
            });
            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = "NikitaViktorov@gmail.com",
                Password = "12345",
                Name = "Nikita",
                Surname = "Viktorov",
                Role = Role.User
            };
            User user1 = new User
            {
                Id = Guid.NewGuid(),
                Email = "Admin@gmail.com",
                Password = "54321",
                Name = "Nikita",
                Surname = "Viktorov",
                Role = Role.Admin
            };
            Tag tag = new Tag { Id = Guid.NewGuid(), Text = "#MU" };
            Tag tag1 = new Tag { Id = Guid.NewGuid(), Text = "#Football" };
            Article article = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Футбольный клуб Манчестер Юнайтед",
                Text = "МЮ - чемпион",
                UserId = user.Id,
            };
            Article article1 = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Английский футбол",
                Text = "Англия - чемпион",
                UserId = user1.Id,
            };
            Article article2 = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Испанский футбол",
                Text = "Испания - чемпион",
                UserId = user.Id,
            };
            Comment comment = new Comment { Id = Guid.NewGuid(), Text = "MU - The Champions!", ArticleId = article.Id};

            modelBuilder.Entity<User>().HasData
            (
                new User[]
                {
                    user,user1
                }
            );
            modelBuilder.Entity<Article>().HasData
             (
                new Article[]
                {
                    article,article1,article2
                }
             );
           
            modelBuilder.Entity<Tag>().HasData(
                new Tag[]
                {
                    tag,tag1
                });

            modelBuilder.Entity<Comment>().HasData(
               new Comment[]
               {
                    comment
               });

            modelBuilder
                .Entity<Article>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Articles)
                .UsingEntity(j => j.HasData(new {ArticlesId = article.Id, TagsId = tag.Id },new { ArticlesId = article1.Id,TagsId = tag.Id}, new { ArticlesId = article2.Id, TagsId = tag1.Id }));
        }
    }
}
    