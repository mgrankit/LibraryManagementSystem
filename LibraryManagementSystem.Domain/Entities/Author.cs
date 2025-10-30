using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;

        public ICollection<Book>? Books { get; set; }
    }
}
