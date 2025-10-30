using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Faculty { get; set; }
        public string? Semester { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
