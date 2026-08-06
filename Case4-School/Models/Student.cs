using System.ComponentModel.DataAnnotations;

namespace Case4_School.Models
{
    public class Student
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        // Navigation Properties
        public StudentProfile? StudentProfile { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();



    }


}
