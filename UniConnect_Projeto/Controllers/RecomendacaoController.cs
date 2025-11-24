using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace UniConnect_Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacaoController : ControllerBase
    {
        private readonly RecomendacaoService _service;
        public RecomendacaoController(RecomendacaoService service) => _service = service;

        [HttpPost("gerar/{alunoId}")]
        public async Task<ActionResult> Gerar(int alunoId)
        {
            var recs = await _service.GerarParaAlunoAsync(alunoId);
            return Ok(recs);
        }

        [HttpGet("{alunoId}")]
        public async Task<ActionResult> Get(int alunoId)
        {
            var recs = await _service.GetPorAlunoAsync(alunoId);
            return Ok(recs);
        }
    }
}