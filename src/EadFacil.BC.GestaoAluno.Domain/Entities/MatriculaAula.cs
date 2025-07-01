using EadFacil.Core.DomainObjects;

namespace EadFacil.BC.GestaoAluno.Domain.Entities;

public sealed class MatriculaAula : Entity
{
    public Guid MatriculaId { get; private set; }
    public Guid AulaId { get; private set; }
    public DateTime? DataDeInicio { get; private set; }
    public DateTime? DataDeConclusao { get; private set; }
}