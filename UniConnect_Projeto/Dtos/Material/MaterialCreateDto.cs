using System.Runtime.InteropServices;
public class MaterialCreateDto
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Url { get; set; }
    public int AutorId { get; set; }
    public int? TurmaId { get; set; }
    public int? GrupoId { get; set; }
}
