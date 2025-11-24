using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Models;

namespace UniConnect_Projeto.Services
{
    public class DisciplinaService
    {
        private readonly DataContext _context;

        public DisciplinaService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<DisciplinaDto>> GetAll()
        {
            return await _context.DisciplinaTable
                .Select(d => new DisciplinaDto
                {
                    Id = d.Id,
                    Nome = d.Nome,
                    CargaHoraria = d.CargaHoraria
                }).ToListAsync();
        }

        public async Task<DisciplinaDto?> GetById(int id)
        {
            var d = await _context.DisciplinaTable.FindAsync(id);
            if (d == null) return null;

            return new DisciplinaDto
            {
                Id = d.Id,
                Nome = d.Nome,
                CargaHoraria = d.CargaHoraria
            };
        }

        public async Task<DisciplinaDto> Create(DisciplinaCreateDto dto)
        {
            // Verificar se o curso existe
            var curso = await _context.CursosTable.FindAsync(dto.CursoId);
            if (curso == null)
                throw new Exception($"Curso com ID {dto.CursoId} não existe.");

            var model = new Disciplina
            {
                Nome = dto.Nome,
                CargaHoraria = dto.CargaHoraria,
                CursoId = dto.CursoId
            };

            _context.DisciplinaTable.Add(model);
            await _context.SaveChangesAsync();

            return new DisciplinaDto
            {
                Id = model.Id,
                Nome = model.Nome,
                CargaHoraria = model.CargaHoraria
            };
        }

        public async Task<bool> Delete(int id)
        {
            var d = await _context.DisciplinaTable.FindAsync(id);
            if (d == null) return false;

            _context.Remove(d);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

