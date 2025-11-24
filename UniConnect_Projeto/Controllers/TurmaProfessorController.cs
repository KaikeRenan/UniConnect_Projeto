using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using UniConnect_Projeto.Services;

namespace UniConnect_Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaProfessorController : ControllerBase
    {
        private readonly TurmaProfessorService _service;

        public TurmaProfessorController(TurmaProfessorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TurmaProfessorDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<TurmaProfessorDto>> Add(TurmaProfessorCreateDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (result == null)
                return Conflict("Professor já está vinculado a esta turma.");

            return Ok(result);
        }

        [HttpDelete("{idProfessor}/{idTurma}")]
        public async Task<IActionResult> Delete(int idProfessor, int idTurma)
        {
            var ok = await _service.DeleteAsync(idProfessor, idTurma);
            if (!ok) return NotFound("Vínculo não encontrado.");
            return NoContent();
        }
    }
}