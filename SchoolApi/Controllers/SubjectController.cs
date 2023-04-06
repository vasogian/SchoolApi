using AutoMapper;
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
    public class SubjectController : ControllerBase
    {
        private readonly SchoolServices _schoolServices;
        private readonly IMapper _mapper;
        public SubjectController(SchoolServices schoolServices, IMapper mapper)
        {
            _schoolServices = schoolServices;
            _mapper = mapper;
        }
        /// <summary>
        /// Get a subject by id.
        /// </summary>
        /// <param name="id">Subject's id.</param>
        [HttpGet]
        public async Task<IActionResult> GetSubjectByid(int id)
        {
            var selectedSubject = await _schoolServices.GetSubjectById(id);
            if (selectedSubject is null)
            {
                return NotFound();
            }
            SubjectViewModel subject = new SubjectViewModel()
            {
                SubjectName = selectedSubject.SubjectName,
                HoursToComplete = selectedSubject.HoursToComplete
            };
            return Ok(subject);
        }
        /// <summary>
        /// Add a new subject.
        /// </summary>
        /// <param name="subject"></param>
        [HttpPost]

        public async Task<IActionResult> AddSubject(CreateOrUpdateSubjectViewModel subject)
        {
            var subjToBeAdded = _mapper.Map<Subjects>(subject);
            if (subjToBeAdded is null)
            {
                return NotFound();
            }
            await this._schoolServices.CreateSubject(subjToBeAdded);
            return CreatedAtAction(nameof(GetSubjectByid), new { Name = subject.SubjectName }, subject);
        }
        /// <summary>
        /// Update a subject's info.
        /// </summary>
        /// <param name="id">Subject's id.</param>
        /// <param name="subject"></param>
        [HttpPut]
        public async Task<IActionResult> UpdateSubject(int id, CreateOrUpdateSubjectViewModel subject)
        {
            var subjectToUpdate = _mapper.Map<Subjects>(subject);
            if (subjectToUpdate is null)
            {
                return NotFound();
            }
            await this._schoolServices.UpdateSubject(id, subjectToUpdate);
            return Ok(subjectToUpdate);
        }
        /// <summary>
        /// Update a field.
        /// </summary>
        /// <param name="id">Subject's id.</param>
        /// <param name="patch"></param>
        [HttpPatch]
        public async Task<IActionResult> PartiallyUpdateSubject(int id,
            JsonPatchDocument<CreateOrUpdateSubjectViewModel> patch)
        {
            if (patch != null)
            {
                var subjectToUpdate = await _schoolServices.GetSubjectById(id);
                var mappedSubject = _mapper.Map<CreateOrUpdateSubjectViewModel>(subjectToUpdate);

                patch.ApplyTo(mappedSubject, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var subjectToBeUpdated = _mapper.Map<Subjects>(mappedSubject);
                var subjectToAdd = await _schoolServices.UpdateSubject(id, subjectToBeUpdated);
                return new ObjectResult(mappedSubject);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Delete a subject.
        /// </summary>
        /// <param name="id">Subject's id.</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subjectToDelete = await _schoolServices.DeleteSubject(id);
            return subjectToDelete is null ? NotFound() : NoContent(); 
        }
    }
}

