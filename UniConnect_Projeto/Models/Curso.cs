namespace UniConnect_Projeto.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public string Coordenador { get; set; }
        public List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
        public ICollection<Turma> Turmas { get; set; } = new List<Turma>();
    }
}
