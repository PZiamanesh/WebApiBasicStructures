using System.Net;
using System.Runtime.Serialization;

namespace WebApplication1.DomainModels
{
    public class Author
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
