using System.ComponentModel.DataAnnotations;

namespace Case4_School.DTOs
{
    public class CourseDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Range(1, 6)]
        public int CreditHours { get; set; }

        public int TeacherId { get; set; }
    }
}
