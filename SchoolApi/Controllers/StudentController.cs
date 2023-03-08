using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;
        public StudentController(SchoolServices schoolServices, IMapper mapper)
        {
            _schoolServices = schoolServices;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var getStudents = await _schoolServices.GetAllStudents();
            if(!getStudents.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<StudentViewModel>>(getStudents));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var searchedStudent = await _schoolServices.GetStudentById(id);
            StudentViewModel studentViewModel = new StudentViewModel()
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(CreateOrUpdateStudentViewModel student)
        {
            var studentToBeCreated = _mapper.Map<Student>(student); 
            if(studentToBeCreated != null)           
            await this._schoolServices.AddStudent(studentToBeCreated);
            return CreatedAtAction(nameof(GetStudentById), new { Name = student.Name }, student);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(int id,CreateOrUpdateStudentViewModel student)
        {
            var  studentToUpdate = _mapper.Map<Student>(student);
            if (studentToUpdate is null)
            {
                return NotFound();
            }
            
            await this._schoolServices.UpdateStudent(id, studentToUpdate);
            return Ok(studentToUpdate);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteStudent(int id)
        {
            var studentToDelete = await _schoolServices.DeleteStudent(id);
            if(studentToDelete is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
