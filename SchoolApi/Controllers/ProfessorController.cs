using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Services;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly SchoolServices _schoolServices;
        public ProfessorController(SchoolServices schoolServices)
        {
            _schoolServices = schoolServices;
        }

        [HttpGet]

        public async Task<IActionResult> GetProfessorById(int id)
        {
            var selectedProfessor = await _schoolServices.GetProfessorById(id);
            if(selectedProfessor is null)
            {
                return NotFound();
            }
            return Ok(selectedProfessor);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfessor(Professor professor)
        {
            await this._schoolServices.CreateProfessor(professor);
            return CreatedAtAction(nameof(GetProfessorById), new { id = professor.ProfId }, professor);
        }
        [HttpPut]

        public async Task<IActionResult> UpdateProfessor(int id, Professor professor)
        {
            var professorToBeUpdated = await _schoolServices.UpdateProfessor(id, professor);
            if(professorToBeUpdated is null)
            {
                return NotFound();
            }
            return Ok(professorToBeUpdated);
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteProfessor(int id)
        {
            var professorToBeDeleted = await _schoolServices.DeleteProfessor(id);
            if(professorToBeDeleted is null)
            {
                return NotFound();
            }
            return NoContent();
        }
        
    }
}
