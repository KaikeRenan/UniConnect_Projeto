using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using UniConnect_Projeto.Services;

namespace UniConnect_Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly CursoService _service;

        public CursoController(CursoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CursoDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> Get(int id)
        {
            var curso = await _service.GetByIdAsync(id);
            if (curso == null) return NotFound("Curso não encontrado.");
            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult<CursoDto>> Create(CursoCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound("Curso não encontrado.");
            return Ok("Curso removido com sucesso.");
        }
    }
}