using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IRepository<Author> _authorRepo;

        public AuthorsController(IRepository<Author> authorRepo)
        {
            _authorRepo = authorRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorRepo.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorDto dto)
        {
            var author = new Author
            {
                AuthorName = dto.AuthorName,
                IsDeleted = dto.IsDeleted
            };

            await _authorRepo.AddAsync(author);
            await _authorRepo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = author.AuthorId }, author);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorDto dto)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null) return NotFound();

            author.AuthorName = dto.AuthorName;
            author.IsDeleted = dto.IsDeleted;

            _authorRepo.Update(author);
            await _authorRepo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null) return NotFound();

            author.IsDeleted = true;
            _authorRepo.Update(author);
            await _authorRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
