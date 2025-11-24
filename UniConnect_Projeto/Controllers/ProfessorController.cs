using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using UniConnect_Projeto.Services;

namespace UniConnect_Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorService _service;

        public ProfessorController(ProfessorService service)
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
        public async Task<ActionResult> Create(ProfessorCreateDto dto)
        {
            var result = await _service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProfessorUpdateDto dto)
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

        [HttpPost("{professorId}/atribuir")]
        public async Task<ActionResult> AtribuirTurma(int professorId, ProfessorSetTurmaDto dto)
        {
            bool ok = await _service.AtribuirTurma(professorId, dto.TurmaId);
            if (!ok) return BadRequest("Professor ou turma inválidos, ou relação já existente.");
            return Ok("Turma atribuída ao professor.");
        }

        [HttpDelete("{professorId}/remover/{turmaId}")]
        public async Task<ActionResult> RemoverTurma(int professorId, int turmaId)
        {
            bool ok = await _service.RemoverTurma(professorId, turmaId);
            if (!ok) return NotFound();
            return Ok("Turma removida do professor.");
        }
    }
}
