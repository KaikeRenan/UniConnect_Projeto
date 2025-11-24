using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace UniConnect_Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoController : ControllerBase
    {
        private readonly GrupoService _service;
        public GrupoController(GrupoService service) => _service = service;

        [HttpPost]
        public async Task<ActionResult<GrupoDto>> Create(GrupoCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoDto>> Get(int id)
        {
            var g = await _service.SearchAsync(null);
            var single = g.FirstOrDefault(x => x.Id == id);
            if (single == null) return NotFound();
            return Ok(single);
        }

        [HttpPost("{id}/entrar")]
        public async Task<ActionResult> Join(int id, GrupoAlunoDto dto)
        {
            var ok = await _service.JoinAsync(id, dto.AlunoId);
            if (!ok) return BadRequest("Não foi possível entrar no grupo (privado ou já membro).");
            return Ok("Entrou no grupo.");
        }

        [HttpPost("{id}/sair")]
        public async Task<ActionResult> Leave(int id, GrupoAlunoDto dto)
        {
            var ok = await _service.LeaveAsync(id, dto.AlunoId);
            if (!ok) return BadRequest("Não foi possível sair do grupo.");
            return Ok("Saiu do grupo.");
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<GrupoDto>>> Search([FromQuery] string? q)
        {
            return Ok(await _service.SearchAsync(q));
        }
    }
}
