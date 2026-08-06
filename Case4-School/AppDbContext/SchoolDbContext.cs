using Case4_School.Models;
using Microsoft.EntityFrameworkCore;

namespace Case4_School.AppDbContext
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasOne(s => s.StudentProfile)
            .WithOne(p => p.Student)
            .HasForeignKey<StudentProfile>(p => p.StudentId);


            modelBuilder.Entity<Course>()
          .HasOne(c => c.Teacher)
          .WithMany(t => t.Courses)
          .HasForeignKey(c => c.TeacherId);

            modelBuilder.Entity<Enrollment>()
           .HasOne(e => e.Student)
           .WithMany(s => s.Enrollments)
           .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);


            modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.CourseId })
            .IsUnique();



            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FullName = "Ahmed Mohamed",
                    Email = "ahmed@example.com",
                    Phone = "01011111111",
                    BirthDate = new DateTime(2004, 5, 10),
                    Gender = "Male"
                },
                new Student
                {
                    Id = 2,
                    FullName = "Sara Ali",
                    Email = "sara@example.com",
                    Phone = "01022222222",
                    BirthDate = new DateTime(2005, 8, 15),
                    Gender = "Female"
                },
                new Student
                {
                    Id = 3,
                    FullName = "Omar Hassan",
                    Email = "omar@example.com",
                    Phone = "01033333333",
                    BirthDate = new DateTime(2003, 12, 20),
                    Gender = "Male"
                }
            );




                modelBuilder.Entity<StudentProfile>().HasData(
                new StudentProfile
                {
                    Id = 1,
                    StudentId = 1,
                    Address = "Cairo, Egypt",
                    ParentPhone = "01111111111",
                    EmergencyContact = "01211111111"
                },
                new StudentProfile
                {
                    Id = 2,
                    StudentId = 2,
                    Address = "Giza, Egypt",
                    ParentPhone = "01122222222",
                    EmergencyContact = "01222222222"
                },
                new StudentProfile
                {
                    Id = 3,
                    StudentId = 3,
                    Address = "Alexandria, Egypt",
                    ParentPhone = "01133333333",
                    EmergencyContact = "01233333333"
                }
            );



            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id = 1,
                    FullName = "Ahmed Hassan",
                    Email = "ahmed.hassan@school.com",
                    Phone = "01011111111",
                    HireDate = new DateTime(2022, 1, 10)
                },
                new Teacher
                {
                    Id = 2,
                    FullName = "Mona Ali",
                    Email = "mona.ali@school.com",
                    Phone = "01022222222",
                    HireDate = new DateTime(2021, 9, 5)
                }
            );




                modelBuilder.Entity<Course>().HasData(
                    new Course
                    {
                        Id = 1,
                        Title = "ASP.NET Core",
                        CreditHours = 3,
                        TeacherId = 1
                    },
                    new Course
                    {
                        Id = 2,
                        Title = "Database Systems",
                        CreditHours = 4,
                        TeacherId = 1
                    },
                    new Course
                    {
                        Id = 3,
                        Title = "Software Engineering",
                        CreditHours = 3,
                        TeacherId = 2
                    }
                );





            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    Id = 1,
                    StudentId = 1,
                    CourseId = 1,
                    EnrollmentDate = new DateTime(2025, 9, 1),
                    Grade = 90
                },
                new Enrollment
                {
                    Id = 2,
                    StudentId = 1,
                    CourseId = 2,
                    EnrollmentDate = new DateTime(2025, 9, 1),
                    Grade = 85
                },
                new Enrollment
                {
                    Id = 3,
                    StudentId = 2,
                    CourseId = 1,
                    EnrollmentDate = new DateTime(2025, 9, 1),
                    Grade = 95
                },
                new Enrollment
                {
                    Id = 4,
                    StudentId = 3,
                    CourseId = 3,
                    EnrollmentDate = new DateTime(2025, 9, 1),
                    Grade = 88
                }
            );
        }



        public DbSet<Student> Students { get; set; }

        public DbSet<StudentProfile> StudentProfiles { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }





    }
}
