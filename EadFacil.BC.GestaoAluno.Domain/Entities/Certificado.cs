using EadFacil.Core.DomainObjects;

namespace EadFacil.BC.GestaoAluno.Domain.Entities;

public sealed class Certificado : Entity
{
    public Guid MatriculaId { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid CursoId { get; private set; }
    public string NomeAluno { get; private set; }
    public string TituloCurso { get; private set; }
    public DateTime DataEmissao { get; private set; }
    public string CodigoVerificacao { get; private set; }
}