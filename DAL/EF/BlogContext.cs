using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;

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
            Tag tag = new Tag { Id = Guid.NewGuid(), Text = "#MU" };
            Article article = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Футбольный клуб Манчестер Юнайтед",
                Text = "МЮ - чемпион",
                UserId = user.Id,
            };
            Comment comment = new Comment { Id = Guid.NewGuid(), Text = "MU - The Champions!", ArticleId = article.Id};

            modelBuilder.Entity<User>().HasData
            (
                new User[]
                {
                    user
                }
            );
            modelBuilder.Entity<Article>().HasData
             (
                new Article[]
                {
                    article
                }
             );
           
            modelBuilder.Entity<Tag>().HasData(
                new Tag[]
                {
                    tag
                });
            modelBuilder.Entity<Comment>().HasData(
               new Comment[]
               {
                    comment
               });
        }
    }
}
