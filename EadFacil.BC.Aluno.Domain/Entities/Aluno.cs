using EadFacil.Core.DomainObjects;

namespace EadFacil.BC.Aluno.Domain.Entities;

public class Aluno : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public Guid UserId { get; private set; }

    // Construtor protegido para EF Core
    protected Aluno() { }

    public Aluno(string nome, string email, string telefone, Guid userId)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        UserId = userId;
        ValidarAluno();
    }   

    public void Atualizar(string nome, string email, string telefone, Guid userId)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        ValidarAluno();
    }

    private void ValidarAluno()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new DomainException("O nome do aluno é obrigatório");

        if (string.IsNullOrWhiteSpace(Email))
            throw new DomainException("O email do aluno é obrigatório");

        if (string.IsNullOrWhiteSpace(Telefone))
            throw new DomainException("O telefone do aluno é obrigatório");
    }
}