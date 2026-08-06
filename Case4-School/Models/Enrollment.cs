using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Case4_School.Models
{
    public class Enrollment
    {


        public int Id { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Range(0, 100)]
        public double? Grade { get; set; }

        // Navigation Properties
        public Student? Student { get; set; }

        public Course? Course { get; set; }
    }
}
