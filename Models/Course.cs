using System;
using System.Collections.Generic;

namespace LoginApiTest.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Instructor { get; set; } = null!;
        public int Duration { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
