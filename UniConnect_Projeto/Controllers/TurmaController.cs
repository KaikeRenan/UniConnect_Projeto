using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using UniConnect_Projeto.Services;

namespace UniConnect_Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly TurmaService _service;
        public TurmaController(TurmaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TurmaDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TurmaDto>> GetById(int id)
        {
            var turma = await _service.GetByIdAsync(id);
            if (turma == null) return NotFound("Turma não encontrada.");
            return Ok(turma);
        }

        [HttpPost]
        public async Task<ActionResult<TurmaDto>> Create(TurmaCreateDto dto)
        {
            var nova = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = nova.Id }, nova);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TurmaDto>> Update(int id, TurmaUpdateDto dto)
        {
            var turma = await _service.UpdateAsync(id, dto);
            if (turma == null) return NotFound("Turma não encontrada.");
            return Ok(turma);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound("Turma não encontrada.");
            return NoContent();
        }
    }
}
