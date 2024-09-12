using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using WebApplication1.DomainModels;
using WebApplication1.Repository;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/authors/{authorId:int}/books")]
    //[Authorize]
    [ApiVersion(2)]
    public class BookController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookController(
            IAuthorRepository authorRepository,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        //[ResponseCache(CacheProfileName = "240sCache")]
        public async Task<IActionResult> GetBooksOfAuthor(int authorId)
        {
            if (!await _authorRepository.IsAuthorExistsAsync(authorId))
            {
                return NotFound();
            }

            var books = await _authorRepository.GetBooksOfAuthor(authorId);
            var booksResult = _mapper.Map<IEnumerable<BookResult>>(books);

            return Ok(booksResult);
        }

        [HttpGet("{bookId:int}", Name = "GetBookOfAuthor")]
        public async Task<IActionResult> GetBookOfAuthor(int authorId, int bookId)
        {
            if (!await _authorRepository.IsAuthorExistsAsync(authorId))
            {
                return NotFound();
            }

            var book = await _authorRepository.GetBookOfAuthor(authorId, bookId);

            if (book is null)
            {
                return NotFound();
            }

            var bookResult = _mapper.Map<BookResult>(book);

            return Ok(bookResult);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookForAuthor(int authorId, CreateBook createBook)
        {
            var author = await _authorRepository.GetAuthorAsync(authorId, false);

            if (author is null)
            {
                return NotFound();
            }

            var book = _mapper.Map<Book>(createBook);
            author.Books.Add(book);
            await _authorRepository.SaveAsync();
            var bookResult = _mapper.Map<BookResult>(book);

            return CreatedAtRoute(
                "GetBookOfAuthor",
                new {AuthorId =  authorId , BookId = bookResult.Id},
                bookResult
                );
        }

        [HttpPut("{bookId:int}")]
        public async Task<IActionResult> UpdateBookOfAuthor(
            int authorId,
            int bookId,
            UpdateBook updateBookDto)
        {
            if (!await _authorRepository.IsAuthorExistsAsync(authorId))
            {
                return NotFound();
            }

            var book = await _authorRepository.GetBookOfAuthor(authorId, bookId);

            if (book is null)
            {
                return NotFound();
            }

            _mapper.Map(updateBookDto, book);
            _authorRepository.UpdateBookOfAuthor(book);
            await _authorRepository.SaveAsync();

            return NoContent();
        }
    }
}
