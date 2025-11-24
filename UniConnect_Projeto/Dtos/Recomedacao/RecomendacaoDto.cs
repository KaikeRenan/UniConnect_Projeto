using System.Runtime.InteropServices;

public class RecomendacaoDto
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public string Mensagem { get; set; }
    public DateTime CriadoEm { get; set; }
    public bool Lida { get; set; }
}
