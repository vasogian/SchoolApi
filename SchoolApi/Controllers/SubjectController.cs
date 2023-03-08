using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]

        public async Task<IActionResult> GetSubjectByid(int id)
        {
            var selectedSubject = await _schoolServices.GetSubjectById(id);
            if(selectedSubject is null)
            {
                return NotFound();
            }
            SubjectViewModel subject = new SubjectViewModel()
            {
                SubjectName = selectedSubject.SubjectName
            };
            return Ok(subject);
        }

        [HttpPost]

        public async Task<IActionResult> AddSubject(CreateOrUpdateSubjectViewModel subject)
        {
           var subjToBeAdded = _mapper.Map<Subjects>(subject);
            if(subjToBeAdded is null)
            {
                return NotFound();
            }
            await this._schoolServices.CreateSubject(subjToBeAdded);
           return CreatedAtAction(nameof(GetSubjectByid), new { Name = subject.SubjectName }, subject);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSubject(int id, CreateOrUpdateSubjectViewModel subject)
        {
            var subjectToUpdate = _mapper.Map<Subjects>(subject);
            if(subjectToUpdate is null)
            {
                return NotFound();
            }
            await this._schoolServices.UpdateSubject(id, subjectToUpdate);
            return Ok(subjectToUpdate);
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteSubject(int id )
        {
            var subjectToDelete = await _schoolServices.DeleteSubject(id);
            if(subjectToDelete is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
