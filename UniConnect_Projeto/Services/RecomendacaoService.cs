using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

public class RecomendacaoService
{
    private readonly DataContext _context;

    public RecomendacaoService(DataContext context) => _context = context;

    public async Task<List<RecomendacaoEstudo>> GerarParaAlunoAsync(int alunoId)
    {
        var aluno = await _context.AlunoTable
            .Include(a => a.Grupos)
                .ThenInclude(g => g.Grupo)
            .FirstOrDefaultAsync(a => a.Id == alunoId);

        if (aluno == null) return new List<RecomendacaoEstudo>();

        var recs = new List<RecomendacaoEstudo>();

        var gruposParticipa = aluno.Grupos.Any();
        if (!gruposParticipa)
        {
            var turmasIds = await _context.TurmaAlunoTable
                .Where(ta => ta.IdAluno == alunoId)
                .Select(ta => ta.IdTurma)
                .ToListAsync();

            var sugestoes = await _context.GrupoEstudoTable
                .Where(g => g.IsPublico && (g.DisciplinaId == null || turmasIds.Contains(g.DisciplinaId.Value)))
                .Take(3)
                .ToListAsync();

            foreach (var g in sugestoes)
            {
                recs.Add(new RecomendacaoEstudo
                {
                    AlunoId = alunoId,
                    Tipo = "Grupo",
                    Mensagem = $"Participe do grupo '{g.Nome}' relacionado a {(g.Disciplina != null ? g.Disciplina.Nome : "temas da turma")}"
                });
            }
        }

        var materiaisPopulares = await _context.MaterialTable
            .Where(m => _context.TurmaAlunoTable
                        .Any(ta => ta.IdAluno == alunoId && ta.IdTurma == m.TurmaId))
            .OrderByDescending(m => m.Likes)
            .Take(3)
            .ToListAsync();

        foreach (var m in materiaisPopulares)
        {
            recs.Add(new RecomendacaoEstudo
            {
                AlunoId = alunoId,
                Tipo = "Material",
                Mensagem = $"Material recomendado: {m.Titulo}"
            });
        }

        if (recs.Any())
        {
            _context.RecomendacaoTable.AddRange(recs);
            await _context.SaveChangesAsync();
        }

        return recs;
    }

    public async Task<List<RecomendacaoEstudo>> GetPorAlunoAsync(int alunoId)
    {
        return await _context.RecomendacaoTable
            .Where(r => r.AlunoId == alunoId)
            .OrderByDescending(r => r.CriadoEm)
            .ToListAsync();
    }
}