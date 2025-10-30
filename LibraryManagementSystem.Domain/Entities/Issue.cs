using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Issue
    {
        public int IssueId { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Book? Book { get; set; }
        public Student? Student { get; set; }
    }
}
