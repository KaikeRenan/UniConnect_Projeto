using System.Runtime.InteropServices;

public class AlunoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public string? CursoNome { get; set; }
    public int? CursoId { get; set; }
}
