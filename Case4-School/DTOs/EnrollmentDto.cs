using System.ComponentModel.DataAnnotations;

namespace Case4_School.DTOs
{
    public class EnrollmentDto
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Range(0, 100)]
        public double? Grade { get; set; }
    }
}
