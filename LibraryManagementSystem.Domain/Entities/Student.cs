using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Faculty { get; set; }
        public string? Semester { get; set; }

        public ICollection<Issue>? Issues { get; set; }
    }
}
