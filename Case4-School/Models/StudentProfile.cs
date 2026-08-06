using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Case4_School.Models
{
    public class StudentProfile

    {

        public int Id { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } 

        [Required]
        [Phone]
        public string ParentPhone { get; set; } 

        [Required]
        [Phone]
        public string EmergencyContact { get; set; } 

        
        public Student? Student { get; set; }
    }
}
