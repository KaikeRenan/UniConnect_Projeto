namespace UniConnect_Projeto.Models
{
    public class Professor : Usuario
    {
        public ICollection<TurmaProfessor> Turmas { get; set; } = new List<TurmaProfessor>();
    }
}
