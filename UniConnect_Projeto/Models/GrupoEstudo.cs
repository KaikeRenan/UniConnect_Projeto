namespace UniConnect_Projeto.Models
{
    public class GrupoEstudo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool IsPublico { get; set; } = true;

        public int CriadorId { get; set; }
        public Aluno Criador { get; set; }

        public int? DisciplinaId { get; set; }    
        public Disciplina Disciplina { get; set; }

        public ICollection<GrupoEstudoAluno> Membros { get; set; } = new List<GrupoEstudoAluno>();
        public ICollection<Material> Materiais { get; set; } = new List<Material>();
    }

    public class GrupoEstudoAluno
    {
        public int GrupoId { get; set; }
        public GrupoEstudo Grupo { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public bool IsModerador { get; set; } = false;
        public DateTime EntrouEm { get; set; } = DateTime.UtcNow;
    }
}