using WebApplication1.DomainModels;
using WebApplication1.ViewModels;

namespace WebApplication1.Repository
{
    public interface IAuthorRepository
    {
        Task<Author?> GetAuthorAsync(int authorId, bool includeBooks);

        Task<(IEnumerable<Author>, PaginationMetadata)> GetAuthorsAsync(int pageNumber, int pageSize, bool includeBooks, string? name);

        Task<bool> IsAuthorExistsAsync(int authorId);

        Author CreateAuthor(Author author);

        Task<IEnumerable<Book>> GetBooksOfAuthor(int authorId);

        Task<Book?> GetBookOfAuthor(int authorId, int bookId);

        Task AddBookForAuthor(int authorId, Book book);

        void UpdateBookOfAuthor(Book book);

        Task<bool> SaveAsync();
    }
}
