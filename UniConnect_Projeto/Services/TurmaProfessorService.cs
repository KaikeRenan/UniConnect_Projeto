using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

namespace UniConnect_Projeto.Services
{
    public class TurmaProfessorService
    {
        private readonly DataContext _context;

        public TurmaProfessorService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TurmaProfessorDto>> GetAllAsync()
        {
            return await _context.TurmaProfessorTable
                .Include(t => t.Professor)
                .Include(t => t.Turma)
                .Select(t => new TurmaProfessorDto
                {
                    IdProfessor = t.IdProfessor,
                    IdTurma = t.IdTurma,
                    NomeProfessor = t.Professor.Nome,
                    NomeTurma = t.Turma.Sigla
                })
                .ToListAsync();
        }

        public async Task<TurmaProfessorDto?> AddAsync(TurmaProfessorCreateDto dto)
        {
            var existe = await _context.TurmaProfessorTable
                .AnyAsync(x => x.IdProfessor == dto.IdProfessor && x.IdTurma == dto.IdTurma);

            if (existe) return null;

            var entity = new TurmaProfessor
            {
                IdProfessor = dto.IdProfessor,
                IdTurma = dto.IdTurma
            };

            _context.TurmaProfessorTable.Add(entity);
            await _context.SaveChangesAsync();

            var professor = await _context.ProfessorTable.FindAsync(dto.IdProfessor);
            var turma = await _context.TurmaTable.FindAsync(dto.IdTurma);

            return new TurmaProfessorDto
            {
                IdProfessor = dto.IdProfessor,
                IdTurma = dto.IdTurma,
                NomeProfessor = professor?.Nome,
                NomeTurma = turma?.Sigla
            };
        }

        public async Task<bool> DeleteAsync(int idProfessor, int idTurma)
        {
            var entity = await _context.TurmaProfessorTable
                .FirstOrDefaultAsync(x => x.IdProfessor == idProfessor && x.IdTurma == idTurma);

            if (entity == null) return false;

            _context.TurmaProfessorTable.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}