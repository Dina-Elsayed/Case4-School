using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Case4_School.Models
{
    public class Course
    {


        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1, 6)]
        public int CreditHours { get; set; }

        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }

       
        public Teacher? Teacher { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

}
