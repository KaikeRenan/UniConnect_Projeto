namespace UniConnect_Projeto.Models
{
    public class TurmaProfessor
    {
        public int IdTurma { get; set; }
        public int IdProfessor { get; set; }
        public Turma Turma { get; set; }
        public Professor Professor { get; set; }
    }
}
