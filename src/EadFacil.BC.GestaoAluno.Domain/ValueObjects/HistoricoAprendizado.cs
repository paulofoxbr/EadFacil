namespace EadFacil.BC.GestaoAluno.Domain.ValueObjects;
public class HistoricoAprendizado
{
    public Guid  MatriculaId { get; private set; }
    public Guid AulaId { get; private set; }
    public DateTime DataDeInicio { get; private set; }
}