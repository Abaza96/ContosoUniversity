namespace ContosoUniversity.Models
{
    public class Enrollment
    {
        public enum Grades
        {
            A, B, C, D, F
        }
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grades? Grade { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}