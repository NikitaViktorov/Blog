using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
            //modelBuilder.Entity<Article>()
            //    .HasMany(c => c.Tags)
            //    .WithMany(s => s.Articles)
            //    .UsingEntity(j => j.ToTable(""));

            //modelBuilder.Entity<Article>()
            //    .HasMany(c => c.Tags)
            //    .WithMany(s => s.Articles)
            //    .UsingEntity(j => j.ToTable("ArticleTag"));

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasConversion(x => x.ToString(), // to converter
                        x => (Role)Enum.Parse(typeof(Role), x));
            });

            modelBuilder.Entity<Article>()
                .HasOne(s => s.Tag)
                .WithMany(g => g.Articles);
            //.HasForeignKey(s => s.TagId);


            modelBuilder.Entity<Article>()
                .HasOne(s => s.User)
                .WithMany(g => g.Articles);

            //modelBuilder.Entity<Article>()
            //    .HasMany(c => c.Tag)
            //    .WithMany(s => s.Articles)
            //    .UsingEntity(j => j.ToTable("ArticleTag"));
            //modelBuilder.Entity<Tag>()
            //    .Property(f => f.Id)
            //    .ValueGeneratedOnAdd();




            //modelBuilder.Entity<Article>()
            //    .HasMany(c => c.Tags)
            //    .WithMany(s => s.Articles)
            //    .UsingEntity(j => j.ToTable("ArticleTag"));




            //modelBuilder.Entity<Article>()
            //    .HasMany(p => p.Tags)
            //    .WithMany(t => t.Articles)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "ArticleTag",
            //        r => r.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
            //        l => l.HasOne<Article>().WithMany().HasForeignKey("ArticleId"),
            //        je =>
            //        {
            //            je.HasKey("ArticleId", "TagId");
            //            je.HasData(
            //                new { ArticleId = 1, TagId = "general" },
            //        });


            //modelBuilder.Entity<Article>()
            //        .HasMany(x => x.Tags)
            //        .WithMany(x => x.Articles)
            //        .UsingEntity<Dictionary<string, object>>(
            //            "EntryTagMaps",
            //            y => y.HasOne<Article>().WithMany(),
            //            x => x.HasOne<Tag>().WithMany());



            //var t1 = Guid.NewGuid();
            //var t2 = Guid.NewGuid();
            //var t3 = Guid.NewGuid();
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
                TagId = tag.Id
            };
            Comment comment = new Comment { Id = Guid.NewGuid(), Text = "MU - The Champions!", ArticleId
                = article.Id};



            //modelBuilder.Entity<Article>()
            //    .HasMany(p => p.Tags)
            //    .WithMany(t => t.Articles)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "ArticleTag",
            //        r => r.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
            //        l => l.HasOne<Article>().WithMany().HasForeignKey("ArticleId"),
            //        je =>
            //        {
            //            je.HasKey("ArticleId", "TagId");
            //            je.HasData(
            //                new { ArticleId = article.Id, TagId = tag.Id });
            //        });




            //article.Tags = new List<Tag>() { new Tag { Id = Guid.NewGuid(), Text = "#MU" } };
            //    modelBuilder.Entity<Tag>().HasData(
            //    new Tag[]
            //    {
            //        new Tag { Text = "dfssd"}       
            //    });
            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.Property(e => e.Role)
            //        .HasMaxLength(50)
            //        .HasConversion(x => x.ToString(), // to converter
            //            x => (Role)Enum.Parse(typeof(Role), x));
            //});
            //modelBuilder.Entity<Tag>()
            //    .Property(f => f.Id)
            //    .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().HasData
            (
                new User[]
                {
                    //new User
                    //{
                    //    Id = t,
                    //    Email = "NikitaViktorov@gmail.com",
                    //    Password = "12345",
                    //    Name = "Nikita",
                    //    Surname = "Viktorov",
                    //    Role = Role.User
                    //}
                    user
                }
                );
            modelBuilder.Entity<Article>().HasData
             (
                new Article[]
                {
                    //new Article
                    //{
                    //    Id = t2,
                    //    Title = "Футбольный клуб Манчестер Юнайтед",
                    //    Text = "МЮ - чемпион",
                    //    UserId = t
                    //    //Tags = new List<Tag>{new Tag { Id = Guid.NewGuid(),Text = "#MU"} }
                    //}
                    article
                }
             );
            //modelBuilder.Entity<Comment>().HasData
            //    (
            //        new Comment[]
            //        {
            //            new Comment
            //            {
            //                Id = Guid.NewGuid(),
            //                Text = "MU-Champions!",
            //                ArticleId = t2
            //            }
            //        }
            //    );
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
            //tag.Articles.Add(article);
            //SaveChangesAsync();
        }
    }
}
