namespace LoginApiTest.Utilities
{
    public class CourseAndEnroRespAPI
    {
        public class CourseDTO
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public string Instructor { get; set; } = null!;
            public int Duration { get; set; }
            public List<EnrollmentDTO> Enrollments { get; set; } = new List<EnrollmentDTO>();
        }

        public class EnrollmentDTO
        {
            public int Id { get; set; }
            public int CourseId { get; set; }
            public int UserId { get; set; }
            public DateTime DateEnrolled { get; set; }
        }

    }
}
