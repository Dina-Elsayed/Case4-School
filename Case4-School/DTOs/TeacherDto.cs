using System.ComponentModel.DataAnnotations;

namespace Case4_School.DTOs
{
    public class TeacherDto
    {

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
        public DateTime HireDate { get; set; }
    }
}
