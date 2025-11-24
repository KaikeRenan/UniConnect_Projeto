using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

namespace UniConnect_Projeto.Services
{
    public class TurmaAlunoService
    {
        private readonly DataContext _context;

        public TurmaAlunoService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TurmaAlunoDto>> GetAllAsync()
        {
            return await _context.TurmaAlunoTable
                .Include(t => t.Aluno)
                .Include(t => t.Turma)
                .Select(t => new TurmaAlunoDto
                {
                    IdAluno = t.IdAluno,
                    IdTurma = t.IdTurma,
                    NomeAluno = t.Aluno.Nome,
                    NomeTurma = t.Turma.Sigla
                })
                .ToListAsync();
        }

        public async Task<TurmaAlunoDto?> AddAsync(TurmaAlunoCreateDto dto)
        {
            var existe = await _context.TurmaAlunoTable
                .AnyAsync(x => x.IdAluno == dto.IdAluno && x.IdTurma == dto.IdTurma);

            if (existe) return null;

            var entity = new TurmaAluno
            {
                IdAluno = dto.IdAluno,
                IdTurma = dto.IdTurma
            };

            _context.TurmaAlunoTable.Add(entity);
            await _context.SaveChangesAsync();

            var aluno = await _context.AlunoTable.FindAsync(dto.IdAluno);
            var turma = await _context.TurmaTable.FindAsync(dto.IdTurma);

            return new TurmaAlunoDto
            {
                IdAluno = dto.IdAluno,
                IdTurma = dto.IdTurma,
                NomeAluno = aluno?.Nome,
                NomeTurma = turma?.Sigla
            };
        }

        public async Task<bool> DeleteAsync(int idAluno, int idTurma)
        {
            var entity = await _context.TurmaAlunoTable
                .FirstOrDefaultAsync(x => x.IdAluno == idAluno && x.IdTurma == idTurma);

            if (entity == null) return false;

            _context.TurmaAlunoTable.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
