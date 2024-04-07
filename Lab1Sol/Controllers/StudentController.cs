using Lab1Sol.DTO;
using Lab1Sol.Entity;
using Lab1Sol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1Sol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private SchoolContext _context { get; }
        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<StudentDTO> students = new List<StudentDTO>();
            foreach (var std in _context.Students.Include(std=> std.Department))
            {
                students.Add(new StudentDTO
                {
                    Id = std.Id,
                    Name = std.Name,
                    Image = std.Image,
                    Age = std.Age,
                    Address = std.Address,
                    Department = std.Department.Name
                });                
            }
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Student student = _context.Students.Include(std => std.Department).FirstOrDefault(std => std.Id == id);
            if (student is null) return NotFound();
            return Ok(new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Image = student.Image,
                Age = student.Age,
                Address = student.Address,
                Department = student.Department.Name,
                deptId = student.DeptId
            });
        }
        [HttpPost]
        public IActionResult Add (Student std)
        {
            if (std is null) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Students.Add(std);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Get), new {id = std.Id }, new { message = "Student Added Succefully" });
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Edit(Student std)
        {
            if (std is null) return NotFound();
            _context.Students.Update(std);
            _context.SaveChanges();
            return Ok(new { message = "Student Updated Successfully" });
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student is null) return NotFound();
            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok(new { student, message = "Student Deleted Successfully" });
        }
    }
}
