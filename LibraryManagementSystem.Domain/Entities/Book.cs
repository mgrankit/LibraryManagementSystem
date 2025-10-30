using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Author? Author { get; set; }
        public ICollection<Issue>? Issues { get; set; }
    }
}
