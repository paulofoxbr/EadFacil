namespace EadFacil.BC.Aluno.Domain.Entities;

public class MatriculaAula
{
    public Guid MatriculaId { get; private set; }
    public Guid AulaId { get; private set; }
    public DateTime? DataDeInicio { get; private set; }
    public DateTime? DataDeConclusao { get; private set; }
}