using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Services;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SchoolServices _schoolServices;
        public StudentController(SchoolServices schoolServices)
        {
            _schoolServices = schoolServices;   
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var getStudents = await _schoolServices.GetAllStudents();
            if(!getStudents.Any())
            {
                return NotFound();
            }
            return Ok(getStudents);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetStudentById(int id)
        {
            var student = await _schoolServices.GetStudentById(id);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult>AddStudent(Student student)
        {
            await this._schoolServices.AddStudent(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }
        [HttpPut]
        public async Task<IActionResult>UpdateStudent(int id, Student student)
        {
            var studentToUpdate = await _schoolServices.UpdateStudent(id, student);
            if(studentToUpdate == null)
            {
                return NotFound();
            }
            return Ok(studentToUpdate);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteStudent(int id)
        {
            var studentToDelete = await _schoolServices.DeleteStudent(id);
            if(studentToDelete == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
