using WebApplication1.DomainModels;

namespace WebApplication1.ViewModels
{
    public class AuthorResult
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<BookResult> Books { get; set; }
    }
}
