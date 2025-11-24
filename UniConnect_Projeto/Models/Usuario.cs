namespace UniConnect_Projeto.Models
{
    public abstract class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
    }
}
