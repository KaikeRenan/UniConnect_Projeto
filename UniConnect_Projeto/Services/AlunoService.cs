using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

namespace UniConnect_Projeto.Services
{
    public class AlunoService
    {
        private readonly DataContext _context;

        public AlunoService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AlunoDto>> GetAll()
        {
            return await _context.AlunoTable
                .Include(a => a.Curso)
                .Select(a => new AlunoDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Email = a.Email,
                    CursoId = a.Curso.Id,
                    CursoNome = a.Curso.Nome
                }).ToListAsync();
        }

        public async Task<AlunoDto?> GetById(int id)
        {
            var aluno = await _context.AlunoTable
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null) return null;

            return new AlunoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Email = aluno.Email,
                CursoId = aluno.Curso.Id,
                CursoNome = aluno.Curso.Nome
            };
        }

        public async Task<AlunoDto?> Create(AlunoCreateDto dto)
        {
            var curso = await _context.CursosTable.FindAsync(dto.CursoId);
            if (curso == null) return null;

            var aluno = new Aluno
            {
                Nome = dto.Nome,
                Email = dto.Email,
                HashPassword = dto.HashPassword,
                Curso = curso
            };

            _context.Add(aluno);
            await _context.SaveChangesAsync();

            return new AlunoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Email = aluno.Email,
                CursoId = curso.Id,
                CursoNome = curso.Nome
            };
        }

        public async Task<bool> Update(int id, AlunoUpdateDto dto)
        {
            var aluno = await _context.AlunoTable
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null) return false;

            var novoCurso = await _context.CursosTable.FindAsync(dto.CursoId);
            if (novoCurso == null) return false;

            aluno.Nome = dto.Nome;
            aluno.Curso = novoCurso;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var aluno = await _context.AlunoTable.FindAsync(id);
            if (aluno == null) return false;

            _context.Remove(aluno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AlunoTurmaDto>> GetTurmas(int alunoId)
        {
            return await _context.TurmaAlunoTable
                .Where(x => x.IdAluno == alunoId)
                .Include(x => x.Turma)
                    .ThenInclude(t => t.Curso)
                .Select(x => new AlunoTurmaDto
                {
                    TurmaId = x.IdTurma,
                    Sigla = x.Turma.Sigla,
                    Ano = x.Turma.Ano,
                    Termo = x.Turma.Termo,
                    CursoNome = x.Turma.Curso.Nome
                })
                .ToListAsync();
        }

        public async Task<bool> Matricular(int alunoId, int turmaId)
        {
            var aluno = await _context.AlunoTable.FindAsync(alunoId);
            var turma = await _context.TurmaTable.FindAsync(turmaId);

            if (aluno == null || turma == null) return false;

            bool jaExiste = await _context.TurmaAlunoTable
                .AnyAsync(t => t.IdAluno == alunoId && t.IdTurma == turmaId);

            if (jaExiste) return false;

            _context.TurmaAlunoTable.Add(new TurmaAluno
            {
                IdAluno = alunoId,
                IdTurma = turmaId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelarMatricula(int alunoId, int turmaId)
        {
            var matricula = await _context.TurmaAlunoTable
                .FirstOrDefaultAsync(t => t.IdAluno == alunoId && t.IdTurma == turmaId);

            if (matricula == null) return false;

            _context.Remove(matricula);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}