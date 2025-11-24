using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace UniConnect_Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialService _service;
        public MaterialController(MaterialService service) => _service = service;

        [HttpPost]
        public async Task<ActionResult<MaterialDto>> Create(MaterialCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialDto>> GetById(int id)
        {
            var m = await _service.GetByIdAsync(id);
            if (m == null) return NotFound();
            return Ok(m);
        }

        [HttpGet("turma/{turmaId}")]
        public async Task<ActionResult<List<MaterialDto>>> ByTurma(int turmaId)
        {
            return Ok(await _service.GetByTurmaAsync(turmaId));
        }

        [HttpGet("grupo/{grupoId}")]
        public async Task<ActionResult<List<MaterialDto>>> ByGrupo(int grupoId)
        {
            return Ok(await _service.GetByGrupoAsync(grupoId));
        }

        [HttpPost("{materialId}/like")]
        public async Task<IActionResult> Like(int materialId, [FromBody] MaterialLikeDto dto)
        {
            var result = await _service.LikeAsync(materialId, dto.AlunoId);
            return Ok(result);
        }
    }
}

