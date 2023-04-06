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
    public class ProfessorController : ControllerBase
    {
        private readonly SchoolServices _schoolServices;
        private readonly IMapper _mapper;
        public ProfessorController(SchoolServices schoolServices, IMapper mapper)
        {
            _schoolServices = schoolServices;
            _mapper = mapper;
        }
        /// <summary>
        /// Get a professor by id.
        /// </summary>
        /// <param name="id">Professor's id.</param>
        [HttpGet]
        public async Task<IActionResult> GetProfessorById(int id)
        {
            var selectedProfessor = await _schoolServices.GetProfessorById(id);
            if (selectedProfessor is null)
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
        /// <summary>
        /// Add a new professor.
        /// </summary>
        /// <param name="professor"></param>
        [HttpPost]
        public async Task<IActionResult> AddProfessor(CreateOrUpdateProfessorViewModel professor)
        {
            var profToBeAdded = _mapper.Map<Professor>(professor);
            if (profToBeAdded is null)
            {
                return NotFound();
            }
            await this._schoolServices.CreateProfessor(profToBeAdded);
            return CreatedAtAction(nameof(GetProfessorById), new { Name = professor.ProfName }, professor);
        }
        /// <summary>
        /// Update a professor's profile information.
        /// </summary>
        /// <param name="id">Professor's id.</param>
        /// <param name="professor"></param>
        [HttpPut]
        public async Task<IActionResult> UpdateProfessor(int id, CreateOrUpdateProfessorViewModel professor)
        {
            var professorToBeUpdated = _mapper.Map<Professor>(professor);
            if (professorToBeUpdated is null)
            {
                return NotFound();
            }
            await this._schoolServices.UpdateProfessor(id, professorToBeUpdated);
            var mappedProfessor = _mapper.Map<CreateOrUpdateProfessorViewModel>(professorToBeUpdated);
            return Ok(mappedProfessor);
        }
        /// <summary>
        /// Update a field.
        /// </summary>
        /// <param name="id">Professor's id.</param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> PartiallyUpdateProfessor(int id,
            JsonPatchDocument<CreateOrUpdateProfessorViewModel> patchDoc)
        {
            if (patchDoc != null)
            {
                var profToUpdate = await _schoolServices.GetProfessorById(id);
                var newProf = _mapper.Map<CreateOrUpdateProfessorViewModel>(profToUpdate);

                patchDoc.ApplyTo(newProf, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var professorToBeUpdated = _mapper.Map<Professor>(newProf);
                var profToAdd = await _schoolServices.UpdateProfessor(id, professorToBeUpdated);
                return new ObjectResult(newProf);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Delete a professor.
        /// </summary>
        /// <param name="id">Professor's id</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            var professorToBeDeleted = await _schoolServices.DeleteProfessor(id);
            return professorToBeDeleted is null ? NotFound() : NoContent();
        }
    }
}
