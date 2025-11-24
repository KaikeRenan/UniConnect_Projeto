using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using UniConnect_Projeto.Services;

namespace UniConnect_Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService _service;
        public AlunoController(AlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AlunoCreateDto dto)
        {
            var result = await _service.Create(dto);
            if (result == null) return BadRequest("Curso inválido");
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AlunoUpdateDto dto)
        {
            bool ok = await _service.Update(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool ok = await _service.Delete(id);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/turmas")]
        public async Task<ActionResult> GetTurmas(int id)
        {
            return Ok(await _service.GetTurmas(id));
        }

        [HttpPost("{alunoId}/matricular")]
        public async Task<ActionResult> Matricular(int alunoId, AlunoMatriculaDto dto)
        {
            bool ok = await _service.Matricular(alunoId, dto.TurmaId);
            if (!ok) return BadRequest("Aluno ou turma inválidos, ou matrícula duplicada.");
            return Ok("Aluno matriculado.");
        }

        [HttpDelete("{alunoId}/cancelar/{turmaId}")]
        public async Task<ActionResult> Cancelar(int alunoId, int turmaId)
        {
            bool ok = await _service.CancelarMatricula(alunoId, turmaId);
            if (!ok) return NotFound();
            return Ok("Matrícula cancelada.");
        }
    }
}
