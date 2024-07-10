using System;
using System.Collections.Generic;

namespace LoginApiTest.Models
{
    public partial class User
    {
        public User()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
