using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Services;
using SchoolApi.ViewModels;

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
        /// <summary>
        /// Get all registered students.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var getStudents = await _schoolServices.GetAllStudents();
            if (!getStudents.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<StudentViewModel>>(getStudents));
        }
        /// <summary>
        /// Get a student by id.
        /// </summary>
        /// <param name="id">Student's id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var searchedStudent = await _schoolServices.GetStudentById(id);
            if (searchedStudent is null)
            {
                return NotFound();
            }

            StudentViewModel student = new StudentViewModel()
            {
                Name = searchedStudent.Name,
                LastName = searchedStudent.LastName,
                Age = searchedStudent.Age
            };
            return Ok(student);
        }
        /// <summary>
        /// Add a student in the database.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddStudent(CreateOrUpdateStudentViewModel student)
        {
            var studentToBeCreated = _mapper.Map<Student>(student);
            if (studentToBeCreated is null)
            {
                return NotFound();
            }
            await this._schoolServices.AddStudent(studentToBeCreated);
            return CreatedAtAction(nameof(GetStudentById), new { Name = student.Name }, student);
        }
        /// <summary>
        /// Update a student's profile information.
        /// </summary>
        /// <param name="id">Student's id.</param>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(int id, CreateOrUpdateStudentViewModel student)
        {
            var studentToUpdate = _mapper.Map<Student>(student);
            if (studentToUpdate is null)
            {
                return NotFound();
            }
            await this._schoolServices.UpdateStudent(id, studentToUpdate);
            return Ok(studentToUpdate);
        }
        /// <summary>
        /// Delete a student.
        /// </summary>
        /// <param name="id">Student's id.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var studentToDelete = await _schoolServices.DeleteStudent(id);
            if (studentToDelete is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}
