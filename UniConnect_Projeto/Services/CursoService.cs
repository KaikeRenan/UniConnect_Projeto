using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

namespace UniConnect_Projeto.Services
{
    public class CursoService
    {
        private readonly DataContext _context;

        public CursoService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CursoDto>> GetAllAsync()
        {
            return await _context.CursosTable
                .Include(c => c.Turmas)
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    CargaHoraria = c.CargaHoraria,
                    Coordenador = c.Coordenador,
                    TotalTurmas = c.Turmas.Count
                })
                .ToListAsync();
        }

        public async Task<CursoDto?> GetByIdAsync(int id)
        {
            return await _context.CursosTable
                .Include(c => c.Turmas)
                .Where(c => c.Id == id)
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    CargaHoraria = c.CargaHoraria,
                    Coordenador = c.Coordenador,
                    TotalTurmas = c.Turmas.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CursoDto> CreateAsync(CursoCreateDto dto)
        {
            var curso = new Curso
            {
                Nome = dto.Nome,
                CargaHoraria = dto.CargaHoraria,
                Coordenador = dto.Coordenador
            };

            _context.CursosTable.Add(curso);
            await _context.SaveChangesAsync();

            return new CursoDto
            {
                Id = curso.Id,
                Nome = curso.Nome,
                CargaHoraria = curso.CargaHoraria,
                Coordenador = curso.Coordenador,
                TotalTurmas = 0
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var curso = await _context.CursosTable.FindAsync(id);
            if (curso == null) return false;

            _context.CursosTable.Remove(curso);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
