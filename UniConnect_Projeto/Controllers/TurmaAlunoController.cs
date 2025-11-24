using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using UniConnect_Projeto.Services;

namespace UniConnect_Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaAlunoController : ControllerBase
    {
        private readonly TurmaAlunoService _service;

        public TurmaAlunoController(TurmaAlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TurmaAlunoDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<TurmaAlunoDto>> Add(TurmaAlunoCreateDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (result == null)
                return Conflict("Aluno já matriculado nesta turma.");

            return Ok(result);
        }

        [HttpDelete("{idAluno}/{idTurma}")]
        public async Task<IActionResult> Delete(int idAluno, int idTurma)
        {
            var ok = await _service.DeleteAsync(idAluno, idTurma);
            if (!ok) return NotFound("Matrícula não encontrada.");
            return NoContent();
        }
    }
}
