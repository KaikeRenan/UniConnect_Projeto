namespace UniConnect_Projeto.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public int Ano { get; set; }
        public int Termo { get; set; }
        public string Sigla { get; set; } // Turma A, Turma B, etc
        public Curso Curso { get; set; }
        public int IdCurso { get; set; }
        public ICollection<TurmaAluno> Alunos { get; set; } = new List<TurmaAluno>();
        public ICollection<TurmaProfessor> Professores { get; set; } = new List<TurmaProfessor>();
    }
}
