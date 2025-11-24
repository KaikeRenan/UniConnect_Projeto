using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

public class MaterialService
{
    private readonly DataContext _context;

    public MaterialService(DataContext context) => _context = context;

    public async Task<MaterialDto> CreateAsync(MaterialCreateDto dto)
    {
        var autor = await _context.AlunoTable.FindAsync(dto.AutorId);
        if (autor == null) throw new Exception("Autor não encontrado");

        if (dto.GrupoId.HasValue)
        {
            var g = await _context.GrupoEstudoTable.FindAsync(dto.GrupoId.Value);
            if (g == null) throw new Exception("Grupo não encontrado");
        }

        if (dto.TurmaId.HasValue)
        {
            var t = await _context.TurmaTable.FindAsync(dto.TurmaId.Value);
            if (t == null) throw new Exception("Turma não encontrada");
        }

        var m = new Material
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Url = dto.Url,
            AutorId = dto.AutorId,
            TurmaId = dto.TurmaId,
            GrupoId = dto.GrupoId
        };

        _context.MaterialTable.Add(m);
        await _context.SaveChangesAsync();

        return new MaterialDto
        {
            Id = m.Id,
            Titulo = m.Titulo,
            Descricao = m.Descricao,
            Url = m.Url,
            AutorId = m.AutorId,
            AutorNome = autor.Nome,
            DataEnvio = m.DataEnvio,
            Likes = m.Likes,
            TurmaId = m.TurmaId,
            GrupoId = m.GrupoId
        };
    }

    public async Task<List<MaterialDto>> GetByTurmaAsync(int turmaId)
    {
        return await _context.MaterialTable
            .Where(m => m.TurmaId == turmaId)
            .Include(m => m.Autor)
            .Select(m => new MaterialDto
            {
                Id = m.Id,
                Titulo = m.Titulo,
                Descricao = m.Descricao,
                Url = m.Url,
                AutorId = m.AutorId,
                AutorNome = m.Autor.Nome,
                DataEnvio = m.DataEnvio,
                Likes = m.Likes,
                TurmaId = m.TurmaId,
                GrupoId = m.GrupoId
            })
            .ToListAsync();
    }

    public async Task<MaterialDto> GetByIdAsync(int id)
    {
        var dto = await _context.MaterialTable
            .Where(m => m.Id == id)
            .Select(m => new MaterialDto
            {
                Id = m.Id,
                Titulo = m.Titulo,
                Descricao = m.Descricao,
                Url = m.Url,
                AutorId = m.AutorId,
                AutorNome = m.Autor.Nome,
                DataEnvio = m.DataEnvio,
                Likes = m.Likes,
                TurmaId = m.TurmaId,
                GrupoId = m.GrupoId
            })
            .FirstOrDefaultAsync();

        if (dto == null)
            throw new KeyNotFoundException($"Material {id} não encontrado.");

        return dto;
    }

    public async Task<List<MaterialDto>> GetByGrupoAsync(int grupoId)
    {
        return await _context.MaterialTable
            .Where(m => m.GrupoId == grupoId)
            .Include(m => m.Autor)
            .Select(m => new MaterialDto
            {
                Id = m.Id,
                Titulo = m.Titulo,
                Descricao = m.Descricao,
                Url = m.Url,
                AutorId = m.AutorId,
                AutorNome = m.Autor.Nome,
                DataEnvio = m.DataEnvio,
                Likes = m.Likes,
                TurmaId = m.TurmaId,
                GrupoId = m.GrupoId
            })
            .ToListAsync();
    }

    public async Task<bool> LikeAsync(int materialId, int alunoId)
    {
        var material = await _context.MaterialTable
            .FirstOrDefaultAsync(m => m.Id == materialId);

        if (material == null)
            return false;

        var aluno = await _context.AlunoTable
            .FirstOrDefaultAsync(a => a.Id == alunoId);

        if (aluno == null)
            return false;

        bool jaCurtiu = await _context.MaterialLikeTable
            .AnyAsync(ml => ml.MaterialId == materialId && ml.AlunoId == alunoId);

        if (jaCurtiu)
            return false;

        var like = new MaterialLike
        {
            MaterialId = materialId,
            AlunoId = alunoId,
            LikedAt = DateTime.UtcNow
        };

        _context.MaterialLikeTable.Add(like);

        material.Likes++;

        await _context.SaveChangesAsync();
        return true;
    }
}