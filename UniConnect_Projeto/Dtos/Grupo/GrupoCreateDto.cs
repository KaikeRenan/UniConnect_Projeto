using System.Runtime.InteropServices;
public class GrupoCreateDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool IsPublico { get; set; } = true;
    public int CriadorId { get; set; }
    public int? DisciplinaId { get; set; }
}
