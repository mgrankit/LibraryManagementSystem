using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs
{
    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
    }
}
