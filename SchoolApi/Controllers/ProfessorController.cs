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
    public class ProfessorController : ControllerBase
    {
        private readonly SchoolServices _schoolServices;
        private readonly IMapper _mapper;
        public ProfessorController(SchoolServices schoolServices, IMapper mapper)
        {
            _schoolServices = schoolServices;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetProfessorById(int id)
        {
            var selectedProfessor = await _schoolServices.GetProfessorById(id);
            if(selectedProfessor is null)
            {
                return NotFound();
            }
            var searchedProfessor = new ProfessorViewModel()
            {
                ProfName = selectedProfessor.ProfName,
                ProfLastName = selectedProfessor.ProfLastName
            };
            return Ok(searchedProfessor);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfessor(CreateOrUpdateProfessorViewModel professor)
        {
            var profToBeAdded = _mapper.Map<Professor>(professor);
            if(professor is null)
            {
                return NotFound();
            }
            await this._schoolServices.CreateProfessor(profToBeAdded);
            return CreatedAtAction(nameof(GetProfessorById), new { Name = professor.ProfName }, professor);
        }
        [HttpPut]

        public async Task<IActionResult> UpdateProfessor(int id, CreateOrUpdateProfessorViewModel professor)
        {
            var professorToBeUpdated =  _mapper.Map<Professor>(professor);
            if (professorToBeUpdated is null)
            {
                return NotFound();
            }
            await this._schoolServices.UpdateProfessor(id, professorToBeUpdated);
           
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
