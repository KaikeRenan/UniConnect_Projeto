namespace UniConnect_Projeto.Models
{
    public class RecomendacaoEstudo
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public string Tipo { get; set; } // e.g. "Material", "Grupo", "Aviso"
        public string Mensagem { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public bool Lida { get; set; } = false;
    }
}