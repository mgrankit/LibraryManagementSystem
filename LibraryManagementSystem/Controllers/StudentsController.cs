using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> _studentRepo;

        public StudentsController(IRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentRepo.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Address = dto.Address,
                ContactNo = dto.ContactNo,
                Faculty = dto.Faculty,
                Semester = dto.Semester,
                IsDeleted = dto.IsDeleted
            };

            await _studentRepo.AddAsync(student);
            await _studentRepo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, student);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentDto dto)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null) return NotFound();

            student.Name = dto.Name;
            student.Address = dto.Address;
            student.ContactNo = dto.ContactNo;
            student.Faculty = dto.Faculty;
            student.Semester = dto.Semester;
            student.IsDeleted = dto.IsDeleted;

            _studentRepo.Update(student);
            await _studentRepo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null) return NotFound();

            student.IsDeleted = true;
            _studentRepo.Update(student);
            await _studentRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
