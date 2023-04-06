using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpPost]
        public async Task<IActionResult> AddStudent(CreateOrUpdateStudentViewModel student)
        {
            var studentToBeCreated = _mapper.Map<Student>(student);
            if (studentToBeCreated is null)
            {
                return NotFound();
            }
            await this._schoolServices.AddStudent(studentToBeCreated);
            return CreatedAtRoute("", new { Name = student.Name }, student);
        }
        /// <summary>
        /// Update a student's profile information.
        /// </summary>
        /// <param name="id">Student's id.</param>
        /// <param name="student"></param>
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
        /// Update a  field 
        /// </summary>
        /// <param name="id">Student's id.</param>
        /// <param name="patch"></param>      
        [HttpPatch]
        public async Task<IActionResult> PartiallyUpdateStudent(int id,
            JsonPatchDocument<CreateOrUpdateStudentViewModel> patch)
        {
            if (patch != null)
            {
                var studentToUpdate = await _schoolServices.GetStudentById(id);
                var newStudent = _mapper.Map<CreateOrUpdateStudentViewModel>(studentToUpdate);

                patch.ApplyTo(newStudent, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var studentToBeUpdated = _mapper.Map<Student>(newStudent);
                var studentToAdd = await _schoolServices.UpdateStudent(id, studentToBeUpdated);
                return new ObjectResult(newStudent);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Delete a student.
        /// </summary>
        /// <param name="id">Student's id.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var studentToDelete = await _schoolServices.DeleteStudent(id);
            return studentToDelete is null ? NotFound() : NoContent();
        }
    }

}
