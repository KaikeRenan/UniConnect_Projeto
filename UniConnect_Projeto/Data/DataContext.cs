using Microsoft.EntityFrameworkCore;
using UniConnect_Projeto.Models;

namespace UniConnect_Projeto.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuario> UsuariosTable { get; set; }
        public DbSet<Aluno> AlunoTable { get; set; }
        public DbSet<Professor> ProfessorTable { get; set; }
        public DbSet<Admin> AdminTable { get; set; }

        public DbSet<Curso> CursosTable { get; set; }
        public DbSet<Disciplina> DisciplinaTable { get; set; }
        public DbSet<Turma> TurmaTable { get; set; }

        public DbSet<TurmaAluno> TurmaAlunoTable { get; set; }
        public DbSet<TurmaProfessor> TurmaProfessorTable { get; set; }

        public DbSet<GrupoEstudo> GrupoEstudoTable { get; set; }
        public DbSet<GrupoEstudoAluno> GrupoEstudoAlunoTable { get; set; }

        public DbSet<Material> MaterialTable { get; set; }
        public DbSet<MaterialLike> MaterialLikeTable { get; set; }

        public DbSet<RecomendacaoEstudo> RecomendacaoTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Turma>()
                .HasOne(t => t.Curso)
                .WithMany(c => c.Turmas)
                .HasForeignKey(t => t.IdCurso)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TurmaAluno>()
                .HasKey(ta => new { ta.IdTurma, ta.IdAluno });

            modelBuilder.Entity<TurmaAluno>()
                .HasOne(ta => ta.Turma)
                .WithMany(t => t.Alunos)
                .HasForeignKey(ta => ta.IdTurma)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TurmaAluno>()
                .HasOne(ta => ta.Aluno)
                .WithMany(a => a.Turmas)
                .HasForeignKey(ta => ta.IdAluno)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TurmaProfessor>()
                .HasKey(tp => new { tp.IdTurma, tp.IdProfessor });

            modelBuilder.Entity<TurmaProfessor>()
                .HasOne(tp => tp.Turma)
                .WithMany(t => t.Professores)
                .HasForeignKey(tp => tp.IdTurma)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TurmaProfessor>()
                .HasOne(tp => tp.Professor)
                .WithMany(p => p.Turmas)
                .HasForeignKey(tp => tp.IdProfessor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GrupoEstudoAluno>()
                .HasKey(g => new { g.GrupoId, g.AlunoId });

            modelBuilder.Entity<GrupoEstudoAluno>()
                .HasOne(g => g.Grupo)
                .WithMany(gr => gr.Membros)
                .HasForeignKey(g => g.GrupoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GrupoEstudoAluno>()
                .HasOne(g => g.Aluno)
                .WithMany(a => a.Grupos)
                .HasForeignKey(g => g.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GrupoEstudo>()
                .HasOne(g => g.Criador)
                .WithMany(a => a.GruposCriados)
                .HasForeignKey(g => g.CriadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GrupoEstudo>()
                .HasOne(g => g.Disciplina)
                .WithMany()
                .HasForeignKey(g => g.DisciplinaId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Turma)
                .WithMany()
                .HasForeignKey(m => m.TurmaId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Grupo)
                .WithMany(g => g.Materiais)
                .HasForeignKey(m => m.GrupoId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Autor)
                .WithMany()
                .HasForeignKey(m => m.AutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MaterialLike>()
                .Property(ml => ml.AlunoId)
                .IsRequired();

            modelBuilder.Entity<MaterialLike>()
                .HasKey(ml => new { ml.MaterialId, ml.AlunoId });

            modelBuilder.Entity<MaterialLike>()
                .HasOne(ml => ml.Material)
                .WithMany(m => m.LikesBy)
                .HasForeignKey(ml => ml.MaterialId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MaterialLike>()
                .HasOne(ml => ml.Aluno)
                .WithMany(a => a.MaterialLikes)
                .HasForeignKey(ml => ml.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecomendacaoEstudo>()
                .HasOne(r => r.Aluno)
                .WithMany(a => a.Recomendacoes)
                .HasForeignKey(r => r.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
