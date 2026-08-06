using System.ComponentModel.DataAnnotations;

namespace Case4_School.DTOs
{
    public class StudentProfileDto
    {

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string ParentPhone { get; set; } 

        [Required]
        [Phone]
        public string EmergencyContact { get; set; } 

        public int StudentId { get; set; }
    }
}
