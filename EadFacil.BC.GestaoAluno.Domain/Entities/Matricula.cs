using EadFacil.Core.DomainObjects;

namespace EadFacil.BC.GestaoAluno.Domain.Entities;

public sealed class Matricula : Entity,IEquatable<Matricula>
{
    public Guid AlunoId { get; private set; }
    public Guid CursoId { get; private set; }
    public DateTime DataMatricula { get; private set; }
    public DateTime? DataConclusao { get; private set; }
    public bool Ativo { get; private set; }
    public bool FinanceiroPago { get; private set; }
    
    // Propriedades de navegação EF Core
    public Aluno Aluno { get; set; }
    // Construtor protegido para EF Core
    protected Matricula() { }
    public Matricula(Guid alunoId, Guid cursoId, DateTime dataMatricula, bool ativo)
    {
        if (alunoId == Guid.Empty)
            throw new DomainException("O ID do aluno é inválido");
            
        if (cursoId == Guid.Empty)
            throw new DomainException("O ID do curso é inválido");
            
        if (dataMatricula > DateTime.Now)
            throw new DomainException("A data de matrícula não pode ser futura");


        AlunoId = alunoId;
        CursoId = cursoId;
        DataMatricula = dataMatricula;
        DataConclusao = null;
        Ativo = ativo;
        FinanceiroPago = false;
    }
    
    public void AtualizarFinanceiroPago(bool pago)
    {
 
        if (DataConclusao.HasValue)
            throw new DomainException("Não é possível alterar o status financeiro de uma matrícula concluída");

        Ativo = pago;
        FinanceiroPago = pago;
    }
    public void Concluir(DateTime dataConclusao)
    {
        if (dataConclusao < DataMatricula)
            throw new DomainException("A data de conclusão não pode ser anterior à data de matrícula.");
        if (DataConclusao.HasValue)
            throw new DomainException("A matrícula já foi concluída.");
        
        DataConclusao = dataConclusao;
        Ativo = false; // Desativa a matrícula ao concluir
    }
    public bool Equals(Matricula? other)
    {
        if (other is null) return false;
        return AlunoId == other.AlunoId && CursoId == other.CursoId;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Matricula);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AlunoId, CursoId);
    }

}