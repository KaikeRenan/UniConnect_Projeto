using System.Runtime.InteropServices;
public class GrupoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool IsPublico { get; set; }
    public int CriadorId { get; set; }
    public string CriadorNome { get; set; }
    public int MembrosCount { get; set; }
    public int? DisciplinaId { get; set; }
    public string? DisciplinaNome { get; set; }
}
