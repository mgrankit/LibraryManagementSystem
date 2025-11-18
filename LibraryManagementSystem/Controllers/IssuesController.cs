using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IRepository<Issue> _issueRepo;

        public IssuesController(IRepository<Issue> issueRepo)
        {
            _issueRepo = issueRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var issues = await _issueRepo.GetAllAsync();
            return Ok(issues);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _issueRepo.GetByIdAsync(id);
            if (issue == null) return NotFound();
            return Ok(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueDto dto)
        {
            var issue = new Issue
            {
                BookId = dto.BookId,
                StudentId = dto.StudentId,
                IssueDate = dto.IssueDate,
            };

            await _issueRepo.AddAsync(issue);
            await _issueRepo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = issue.IssueId }, issue);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueDto dto)
        {
            var issue = await _issueRepo.GetByIdAsync(id);
            if (issue == null) return NotFound();

            issue.BookId = dto.BookId;
            issue.StudentId = dto.StudentId;
            issue.IssueDate = dto.IssueDate;

            _issueRepo.Update(issue);
            await _issueRepo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var issue = await _issueRepo.GetByIdAsync(id);
            if (issue == null) return NotFound();
            _issueRepo.Update(issue);
            await _issueRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
