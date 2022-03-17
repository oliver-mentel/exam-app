using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ExamApp.Context;
public class MainContext : DbContext
{
    // Method never is never invoked.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Uncomment for dev
        optionsBuilder.UseInMemoryDatabase("Dev");

        // Either by adding a class Configuration.cs or through the lines bellow.
        Configuration configuration1 = new Configuration();
        configuration1.GetConfiguration();

        // Use the Secret Manager Tool to store sensitive data (password, username) safely.
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        string connectionString = configuration.GetConnectionString("Student");
        var builder = optionsBuilder.UseSqlServer(connectionString);

        // Comment for dev
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Language> Languages { get; set; }

    // Method never is never invoked.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasMany(d => d.Students)
                .WithOne(p => p.Course)
                .HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.Language);
        });
    }
}

public class Course
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    // Description is never used
    public string Description { get; set; }

    public virtual ICollection<Student> Students { get; set; }
    public virtual Language Language { get; set; }
}

public class Student
{
    [Key]
    public int Id { get; set; }
    // Name is never used
    public string Name { get; set; }
    public int Age { get; set; }
    public Guid CourseId { get; set; }

    public virtual Course Course { get; set; }
}

public record Language(Guid Id, string Title);