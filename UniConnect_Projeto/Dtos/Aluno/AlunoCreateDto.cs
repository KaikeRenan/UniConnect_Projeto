using System.Runtime.InteropServices;

public class AlunoCreateDto 
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }
    public int? CursoId { get; set; }
}
