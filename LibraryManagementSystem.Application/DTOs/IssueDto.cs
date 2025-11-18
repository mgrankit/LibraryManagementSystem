using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs
{
    public class IssueDto
    {
        public int IssueId { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    }
}
