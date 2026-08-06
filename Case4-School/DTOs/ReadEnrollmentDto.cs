namespace Case4_School.DTOs
{
    public class ReadEnrollmentDto
    {

        public int Id { get; set; }

        public string StudentName { get; set; } 

        public string CourseTitle { get; set; } 

        public DateTime EnrollmentDate { get; set; }

        public double? Grade { get; set; }
    }
}
