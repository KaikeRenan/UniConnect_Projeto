using System.Runtime.InteropServices;
using UniConnect_Projeto.Models;

public class TurmaDto
{
    public int Id { get; set; }
    public int IdCurso { get; set; }
    public string Sigla { get; set; }
    public List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
}
