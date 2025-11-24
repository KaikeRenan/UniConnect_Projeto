namespace UniConnect_Projeto.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }             
        public int AutorId { get; set; }            
        public Aluno Autor { get; set; }
        public DateTime DataEnvio { get; set; } = DateTime.UtcNow;
        public int? TurmaId { get; set; }
        public Turma Turma { get; set; }
        public int? GrupoId { get; set; }
        public GrupoEstudo Grupo { get; set; }
        public int Likes { get; set; } = 0;
        public ICollection<MaterialLike> LikesBy { get; set; } = new List<MaterialLike>();
        public int Version { get; set; } = 1;
    }

    public class MaterialLike
    {
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
    }
}