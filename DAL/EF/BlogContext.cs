using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.EF
{
    public class BlogContext : DbContext
    {
        //public BlogContext()
        //{
        //    Database.EnsureCreated();
        //}
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        optionsBuilder.UseSqlServer("Server=DESKTOP-NII5L4A;Database=Blog;Trusted_Connection=True;");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    modelBuilder.Entity<Tag>().HasData(
            //    new Tag[]
            //    {
            //        new Tag { Text = "dfssd"}       
            //    });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasConversion(x => x.ToString(), // to converter
                        x => (Role)Enum.Parse(typeof(Role), x));
            });
            modelBuilder.Entity<Tag>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            var t = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData
             (
                new User[]
                {
                    new User
                    {
                        Id = t,
                        Email = "NikitaViktorov@gmail.com",
                        Password = "12345",
                        Name = "Nikita",
                        Surname = "Viktorov",
                        Role = Role.User
                    }
                });
            var t2 = Guid.NewGuid();
            modelBuilder.Entity<Article>().HasData
             (
                new Article[]
                {
                    new Article
                    {
                        Id = t2,
                        Title = "Футбольный клуб Манчестер Юнайтед",
                        Text = "МЮ - чемпион",
                        UserId = t
                    }
                }
             );
            modelBuilder.Entity<Comment>().HasData
                (
                    new Comment[]
                    {
                        new Comment
                        {
                            Id = Guid.NewGuid(),
                            Text = "Лалалаа",
                            ArticleId = t2
                        }
                    }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag[]
                {
                    new Tag { Id = Guid.NewGuid(), Text = "dfssd"}
                });

        }
    }
}
