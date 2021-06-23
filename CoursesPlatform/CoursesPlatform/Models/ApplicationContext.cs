using CoursesPlatform.Models.Teacher.Course;
using CoursesPlatform.Models.Teacher.Course.Elements;
using CoursesPlatform.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        
        public DbSet<ContentElement> ContentElements { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }

        public ApplicationContext(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Course>()
                .HasOne(e => e.UserStudiedCourses)
                .WithMany(e => e.StudiedCourses)
                .HasForeignKey(e => e.UserStudiedCoursesId);

            builder.Entity<Course>()
                .HasOne(e => e.UserPublishedCourses)
                .WithMany(e => e.PublishedCourses)
                .HasForeignKey(e => e.UserPublishedCoursesId);

            builder.Entity<ContentElement>()
                .HasOne(p => p.Lesson)
                .WithMany(t => t.Content)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
