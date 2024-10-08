﻿using Asp.Versioning;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Reflection;
using System.Text.Json;
using WebApplication1.DomainModels;
using WebApplication1.Filters;
using WebApplication1.Repository;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/authors")]
    //[Authorize("mustBeZia")]
    [ApiVersion(1)]
    [ApiVersion(2)]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(
            IAuthorRepository authorRepository,
            IMapper mapper
            )
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        //[ResponseCache(Duration = 120)]
        //[HttpCacheExpiration(MaxAge = 100, CacheLocation = CacheLocation.Public)]
        //[HttpCacheValidation(MustRevalidate = true)]
        public async Task<ActionResult<IEnumerable<AuthorResult>>> GetAuthors(
            bool includeBooks, 
            string? name,
            int pageNumber,
            int pageSize)
        {
            var (authors, paginationInfo ) = await _authorRepository.GetAuthorsAsync(pageNumber, pageSize, includeBooks, name);
            var authorsResult = _mapper.Map<IEnumerable<AuthorResult>>(authors);
            HttpContext.Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationInfo));

            return Ok(authorsResult);
        }

        [HttpGet("Stream")]
        public async IAsyncEnumerable<Author> GetAuthorsAsStream()
        {
            await foreach (var author in _authorRepository.GetAuthorsAsyncEnumerable())
            {
                yield return author;
            }
        }

        [HttpGet("{authorId:int}", Name = "CreateAuthor")]
        [TypeFilter<AuthorResultFilter>()]
        public async Task<IActionResult> GetAuthor(int authorId, bool includeBooks, CancellationToken token)
        {
            if (!await _authorRepository.IsAuthorExistsAsync(authorId))
            {
                return NotFound();
            }

            var author = await _authorRepository.GetAuthorAsync(authorId, token, includeBooks);
            //var authorResult = _mapper.Map<AuthorResult>(author);

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorResult>> CreateAuthor(CreateAuthor createAuthor)
        {
            var author = _mapper.Map<Author>(createAuthor);
            var newAuthor = _authorRepository.CreateAuthor(author);
            await _authorRepository.SaveAsync();
            var authorDto = _mapper.Map<AuthorResult>(newAuthor);

            return CreatedAtRoute(
                "CreateAuthor",
                new { authorId = newAuthor.Id, includeBooks = createAuthor.Books.Any() },
                authorDto
                );
        }
    }
}
