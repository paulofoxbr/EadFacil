namespace EadFacil.BC.Aluno.Domain.Entities;

public class Matricula
{
    public Guid AlunoId { get; private set; }
    public Guid CursoId { get; private set; }
    public DateTime DataMatricula { get; private set; }
    public bool Ativo { get; private set; }
}