using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication1.DataAccess;
using WebApplication1.DomainModels;
using WebApplication1.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AspContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthorRepository(AspContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Author?> GetAuthorAsync(
            int id,
            CancellationToken token,
            bool includeBooks = false)
        {
            if (includeBooks)
            {
                return await _context.Authors
                    .Include(a => a.Books)
                    .FirstOrDefaultAsync(a => a.Id == id);
            }

            var result = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            return result;
        }

        public async Task<(IEnumerable<Author>, PaginationMetadata)> GetAuthorsAsync(
            int pageNumber,
            int pageSize,
            bool includeBooks = false,
            string? name = null)
        {
            const int maxPageSize = 34;

            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 3;
            }

            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var authors = _context.Authors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                authors = authors.Where(a => a.AuthorName == name);
            }

            if (includeBooks)
            {
                authors = _context.Authors.Include(a => a.Books);
            }

            var authorsResult = await authors
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var totalItems = await _context.Authors.CountAsync();
            var paginationInfo = new PaginationMetadata(totalItems, pageSize, pageNumber);

            return (authorsResult, paginationInfo);
        }

        public async Task<bool> IsAuthorExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public Author CreateAuthor(Author author)
        {
            var result = _context.Authors.Add(author);
            return result.Entity;
        }

        public async Task<Book?> GetBookOfAuthor(int authorId, int bookId)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.AuthorId == authorId && b.Id == bookId);
        }

        public async Task<IEnumerable<Book>> GetBooksOfAuthor(int authorId)
        {
            return await _context.Books.Where(b => b.AuthorId == authorId).ToListAsync();
        }

        public async Task AddBookForAuthor(int authorId, Book book)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);

            if (author is { })
            {
                author.Books.Add(book);
            }
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateBookOfAuthor(Book book)
        {
            _context.Update(book);
        }

        public IAsyncEnumerable<Author> GetAuthorsAsyncEnumerable()
        {
            return _context.Authors.Include(a => a.Books).AsAsyncEnumerable<Author>();
        }

        public async Task<Author?> GetAuthorAsync(int authorId, bool includeBooks)
        {
            if (includeBooks)
            {
                return await _context.Authors
                    .Include(a => a.Books)
                    .FirstOrDefaultAsync(a => a.Id == authorId);
            }

            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
        }
    }
}
