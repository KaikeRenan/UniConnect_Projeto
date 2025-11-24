namespace UniConnect_Projeto.Models
{
    public class TurmaAluno
    {
        public int IdTurma { get; set; }
        public int IdAluno { get; set; }
        public Turma Turma { get; set; }
        public Aluno Aluno { get; set; }
    }
}
