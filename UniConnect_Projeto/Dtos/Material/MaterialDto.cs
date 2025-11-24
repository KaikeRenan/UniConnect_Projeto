using System.Runtime.InteropServices;
public class MaterialDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Url { get; set; }
    public int AutorId { get; set; }
    public string AutorNome { get; set; }
    public DateTime DataEnvio { get; set; }
    public int Likes { get; set; }
    public int? TurmaId { get; set; }
    public int? GrupoId { get; set; }
}
