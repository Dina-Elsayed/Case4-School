using System.ComponentModel.DataAnnotations;

namespace Case4_School.DTOs
{
    public class StudentDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; } 

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
