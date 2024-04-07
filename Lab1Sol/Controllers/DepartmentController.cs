using Lab1Sol.DTO;
using Lab1Sol.Entity;
using Lab1Sol.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1Sol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private SchoolContext context { get; }
        public DepartmentController(SchoolContext _context)
        {
            context = _context;            
        }
        [HttpGet]
        public IActionResult AllDepartments()
        {
            List<DepartmentDTO> departmentDTOs = new List<DepartmentDTO>();
            context.Departments.Include(dept => dept.Students).ToList().ForEach(dept =>
            {
                DepartmentDTO deptDto = new DepartmentDTO();
                deptDto.Id = dept.Id;
                deptDto.Name = dept.Name;
                deptDto.Manger = dept.MangerName;
                deptDto.Location = dept.Location;
                foreach (var deptStd in dept.Students.Where(std => std.DeptId == dept.Id))
                {
                    deptDto.StdsName.Add(deptStd.Name);
                }
                departmentDTOs.Add(deptDto);
            });
            return Ok(departmentDTOs);
        }
        [HttpGet("{id:int}", Name ="DeptById")]
        public IActionResult DepartmentById(int id)
        {
            var dept = context.Departments.Include(dept => dept.Students).FirstOrDefault(dept => dept.Id == id);
            if (dept is null) return NotFound();
            DepartmentDTO deptDto = new DepartmentDTO();
            deptDto.Id = dept.Id;
            deptDto.Name = dept.Name;
            deptDto.Manger = dept.MangerName;
            deptDto.Location = dept.Location;
            foreach (var deptStd in dept.Students.Where(std => std.DeptId == dept.Id))
            {
                deptDto.StdsName.Add(deptStd.Name);
            }
            return Ok(deptDto);
        }

        [HttpPost]
        public IActionResult AddDepartment(Department dept)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Add(dept);
                context.SaveChanges();
                return CreatedAtRoute("DeptById", new { id = dept.Id }, dept);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        public IActionResult EditDepartment(int id, Department dept)
        {
            Department oldDept = context.Departments.FirstOrDefault(dept => dept.Id == id);
            if (oldDept is null) return NotFound();
            oldDept.Name = dept.Name;
            oldDept.MangerName = dept.MangerName;
            oldDept.Location = dept.Location;
            oldDept.OpenDate = dept.OpenDate;
            context.SaveChanges();
            return Ok(oldDept);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)
        {
            Department dept = context.Departments.FirstOrDefault(dept => dept.Id == id);
            if (dept is null) return NotFound();
            context.Departments.Remove(dept);
            context.SaveChanges();
            return Ok(dept);
        }
    }
}
