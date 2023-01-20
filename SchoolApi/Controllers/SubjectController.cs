using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Services;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SchoolServices _schoolServices;
        public SubjectController(SchoolServices schoolServices)
        {
            _schoolServices = schoolServices;
        }

        [HttpGet]

        public async Task<IActionResult> GetSubjectByid(int id)
        {
            var selectedSubject = await _schoolServices.GetSubjectById(id);
            if(selectedSubject is null)
            {
                return NotFound();
            }
            return Ok(selectedSubject);
        }

        [HttpPost]

        public async Task<IActionResult> AddSubject(Subjects subject)
        {
           await this._schoolServices.CreateSubject(subject);
           return CreatedAtAction(nameof(GetSubjectByid), new { subject.SubjectId }, subject);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSubject(int id, Subjects subject)
        {
            var subjectToUpdate = await _schoolServices.UpdateSubject(id, subject);
            if(subjectToUpdate is null)
            {
                return NotFound();
            }
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
