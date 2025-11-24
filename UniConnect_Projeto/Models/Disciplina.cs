namespace UniConnect_Projeto.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
