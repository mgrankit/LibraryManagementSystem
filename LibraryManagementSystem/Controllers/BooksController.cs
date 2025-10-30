using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepo;

        public BooksController(IRepository<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _bookRepo.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDto dto)
        {
            var book = new Book { Title = dto.Title, AuthorId = dto.AuthorId };
            await _bookRepo.AddAsync(book);
            await _bookRepo.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = book.BookId }, book);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return NotFound();

            book.Title = dto.Title;
            book.AuthorId = dto.AuthorId;
            _bookRepo.Update(book);
            await _bookRepo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return NotFound();

            // soft delete pattern:
            book.IsDeleted = true;
            _bookRepo.Update(book);
            await _bookRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
