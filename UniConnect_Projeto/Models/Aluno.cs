namespace UniConnect_Projeto.Models
{
    public class Aluno : Usuario
    {
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public ICollection<TurmaAluno> Turmas { get; set; } = new List<TurmaAluno>();
        public ICollection<GrupoEstudoAluno> Grupos { get; set; } = new List<GrupoEstudoAluno>();
        public ICollection<GrupoEstudo> GruposCriados { get; set; } = new List<GrupoEstudo>();
        public ICollection<RecomendacaoEstudo> Recomendacoes { get; set; } = new List<RecomendacaoEstudo>();
        public ICollection<MaterialLike> MaterialLikes { get; set; } = new List<MaterialLike>();
    }
}
