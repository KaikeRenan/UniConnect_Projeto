using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

namespace UniConnect_Projeto.Services
{
    public class TurmaService
    {
        private readonly DataContext _context;

        public TurmaService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TurmaDto>> GetAllAsync()
        {
            return await _context.TurmaTable
                .Select(turma => new TurmaDto
                {
                    Id = turma.Id,
                    Sigla = turma.Sigla,
                    IdCurso = turma.Curso.Id,
                    Disciplinas = turma.Curso.Disciplinas
                }).ToListAsync();
        }

        public async Task<TurmaDto?> GetByIdAsync(int id)
        {
            var turma = await _context.TurmaTable.FindAsync(id);
            if (turma == null) return null;

            return new TurmaDto
            {
                Id = turma.Id,
                Sigla = turma.Sigla,
                IdCurso = turma.Curso.Id,
                Disciplinas = turma.Curso.Disciplinas
            };
        }

        public async Task<TurmaDto> CreateAsync(TurmaCreateDto dto)
        {
            var turma = new Turma
            {
                Sigla = dto.Sigla,
                IdCurso = dto.IdCurso
            };

            _context.TurmaTable.Add(turma);
            await _context.SaveChangesAsync();

            return new TurmaDto
            {
                Id = turma.Id,
                Sigla = turma.Sigla,
                IdCurso = turma.IdCurso
            };
        }

        public async Task<TurmaDto?> UpdateAsync(int id, TurmaUpdateDto dto)
        {
            var turma = await _context.TurmaTable.FindAsync(id);
            if (turma == null) return null;

            turma.Sigla = dto.Sigla;
            turma.IdCurso = dto.IdCurso;

            await _context.SaveChangesAsync();

            return new TurmaDto
            {
                Id = turma.Id,
                Sigla = turma.Sigla,
                IdCurso = turma.IdCurso
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var turma = await _context.TurmaTable.FindAsync(id);
            if (turma == null) return false;

            _context.TurmaTable.Remove(turma);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
