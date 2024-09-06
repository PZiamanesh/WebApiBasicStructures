using WebApplication1.DomainModels;

namespace WebApplication1.ViewModels
{
    public class CreateAuthor
    {
        public string AuthorName { get; set; }

        public IEnumerable<CreateBook> Books { get; set; } = new HashSet<CreateBook>();
    }
}
