using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

namespace UniConnect_Projeto.Services
{
    public class ProfessorService
    {
        private readonly DataContext _context;

        public ProfessorService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProfessorDto>> GetAll()
        {
            return await _context.ProfessorTable
                .Select(p => new ProfessorDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Email = p.Email
                }).ToListAsync();
        }

        public async Task<ProfessorDto?> GetById(int id)
        {
            var professor = await _context.ProfessorTable.FindAsync(id);

            if (professor == null) return null;

            return new ProfessorDto
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Email = professor.Email
            };
        }

        public async Task<ProfessorDto> Create(ProfessorCreateDto dto)
        {
            var professor = new Professor
            {
                Nome = dto.Nome,
                Email = dto.Email,
                HashPassword = dto.HashPassword
            };

            _context.ProfessorTable.Add(professor);
            await _context.SaveChangesAsync();

            return new ProfessorDto 
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Email = professor.Email
            };
        }

        public async Task<bool> Update(int id, ProfessorUpdateDto dto)
        {
            var professor = await _context.ProfessorTable.FindAsync(id);
            if (professor == null) return false;

            professor.Nome = dto.Nome;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var professor = await _context.ProfessorTable.FindAsync(id);
            if (professor == null) return false;

            _context.Remove(professor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProfessorTurmaDto>> GetTurmas(int professorId)
        {
            return await _context.TurmaProfessorTable
                .Where(t => t.IdProfessor == professorId)
                .Include(t => t.Turma)
                .ThenInclude(t => t.Curso)
                .Select(t => new ProfessorTurmaDto
                {
                    TurmaId = t.Turma.Id,
                    Sigla = t.Turma.Sigla,
                    Ano = t.Turma.Ano,
                    Termo = t.Turma.Termo,
                    CursoNome = t.Turma.Curso.Nome
                })
                .ToListAsync();
        }

        public async Task<bool> AtribuirTurma(int professorId, int turmaId)
        {
            var professor = await _context.ProfessorTable.FindAsync(professorId);
            var turma = await _context.TurmaTable.FindAsync(turmaId);

            if (professor == null || turma == null) return false;

            bool jaExiste = await _context.TurmaProfessorTable
                .AnyAsync(t => t.IdProfessor == professorId && t.IdTurma == turmaId);

            if (jaExiste) return false;

            _context.TurmaProfessorTable.Add(new TurmaProfessor
            {
                IdProfessor = professorId,
                IdTurma = turmaId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverTurma(int professorId, int turmaId)
        {
            var rel = await _context.TurmaProfessorTable
                .FirstOrDefaultAsync(t => t.IdProfessor == professorId && t.IdTurma == turmaId);

            if (rel == null) return false;

            _context.Remove(rel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
