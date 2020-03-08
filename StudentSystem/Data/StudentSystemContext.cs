using Microsoft.EntityFrameworkCore;
using StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {

        }
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnConfiguringCourse(modelBuilder);

            OnConfiguringResource(modelBuilder);
            OnConfiguringHomework(modelBuilder); 
            OnConfiguringStudentCourse(modelBuilder);

        }

        private void OnConfiguringStudentCourse(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<StudentCourse>(entity =>
              {
                  entity
                    .HasKey(e => new { e.StudentId, e.CourseId});

                  entity
                  .HasOne(e => e.Student)
                  .WithMany(s => s.CourseEnrollments)
                  .HasForeignKey(e => e.CourseId);
                                   
              });
       }

        private void OnConfiguringHomework(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<Homework>(entity =>
               {
                   entity
                     .HasKey(e => e.HomeworkId);

                   entity
                   .Property(e => e.Content)
                   .IsRequired();

                   entity
                   .HasOne(e => e.Student)
                   .WithMany(e => e.HomeworkSubmissions);

                   entity
                  .HasOne(e => e.Course)
                  .WithMany(e => e.HomeworkSubmissions);
               });
        }

        private void OnConfiguringResource(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Resource>(entity =>
                {
                    entity
                      .HasKey(e => e.ResourceId);

                    entity
                    .Property(e=>e.Name)
                    .HasMaxLength(80)
                      .IsUnicode()
                      .IsRequired();

                    entity
                    .Property(e => e.Url)
                    .IsRequired();

                    entity
                    .HasOne(e => e.Course)
                    .WithMany(e => e.Resources);
                                        
                });
        }

        private void OnConfiguringCourse(ModelBuilder modelBuilder)
        {
            modelBuilder
                  .Entity<Course>(entity =>
                  {
                      entity
                      .HasKey(e => e.CourseId);

                      entity.
                      Property(e => e.Name)
                      .HasMaxLength(80)
                      .IsUnicode()
                      .IsRequired();

                      entity
                      .Property(e => e.Description)
                      .IsUnicode();

                  });
        }
    }
}
