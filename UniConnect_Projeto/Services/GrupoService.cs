using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

public class GrupoService
{
    private readonly DataContext _context;
    public GrupoService(DataContext context) => _context = context;

    public async Task<GrupoDto> CreateAsync(GrupoCreateDto dto)
    {
        var criador = await _context.AlunoTable.FindAsync(dto.CriadorId);
        if (criador == null) throw new Exception("Criador não encontrado");

        var grupo = new GrupoEstudo
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            IsPublico = dto.IsPublico,
            CriadorId = dto.CriadorId,
            DisciplinaId = dto.DisciplinaId
        };

        _context.GrupoEstudoTable.Add(grupo);
        await _context.SaveChangesAsync();

        // add creator as member + moderador
        _context.GrupoEstudoAlunoTable.Add(new GrupoEstudoAluno
        {
            GrupoId = grupo.Id,
            AlunoId = dto.CriadorId,
            IsModerador = true
        });
        await _context.SaveChangesAsync();

        return new GrupoDto
        {
            Id = grupo.Id,
            Nome = grupo.Nome,
            Descricao = grupo.Descricao,
            IsPublico = grupo.IsPublico,
            CriadorId = grupo.CriadorId,
            CriadorNome = criador.Nome,
            MembrosCount = 1,
            DisciplinaId = dto.DisciplinaId
        };
    }

    public async Task<bool> JoinAsync(int grupoId, int alunoId)
    {
        var grupo = await _context.GrupoEstudoTable.FindAsync(grupoId);
        if (grupo == null) return false;

        bool exists = await _context.GrupoEstudoAlunoTable
            .AnyAsync(x => x.GrupoId == grupoId && x.AlunoId == alunoId);
        if (exists) return false;

        if (!grupo.IsPublico)
        {
            return false;
        }

        _context.GrupoEstudoAlunoTable.Add(new GrupoEstudoAluno
        {
            GrupoId = grupoId,
            AlunoId = alunoId
        });
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> LeaveAsync(int grupoId, int alunoId)
    {
        var rel = await _context.GrupoEstudoAlunoTable
            .FirstOrDefaultAsync(x => x.GrupoId == grupoId && x.AlunoId == alunoId);
        if (rel == null) return false;

        _context.GrupoEstudoAlunoTable.Remove(rel);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<GrupoDto>> SearchAsync(string? q)
    {
        var query = _context.GrupoEstudoTable.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(g => g.Nome.Contains(q) || g.Descricao.Contains(q));

        return await query
            .Include(g => g.Criador)
            .Include(g => g.Disciplina)
            .Select(g => new GrupoDto
            {
                Id = g.Id,
                Nome = g.Nome,
                Descricao = g.Descricao,
                IsPublico = g.IsPublico,
                CriadorId = g.CriadorId,
                CriadorNome = g.Criador.Nome,
                MembrosCount = g.Membros.Count,
                DisciplinaId = g.DisciplinaId,
                DisciplinaNome = g.Disciplina != null ? g.Disciplina.Nome : null
            })
            .ToListAsync();
    }
}