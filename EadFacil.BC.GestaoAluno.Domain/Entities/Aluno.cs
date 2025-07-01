using EadFacil.Core.DomainObjects;

namespace EadFacil.BC.GestaoAluno.Domain.Entities;

public sealed class Aluno : Entity, IAggregateRoot
{
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefone { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    
    private readonly List<Matricula> _matriculas = new List<Matricula>();
    private readonly List<Certificado> _certificados = new List<Certificado>();
    public IReadOnlyCollection<Matricula> Matriculas=> _matriculas.AsReadOnly();
    public IReadOnlyCollection<Certificado> Certificados => _certificados.AsReadOnly();

    // Construtor protegido para EF Core
    protected Aluno() { }

    public Aluno(string nome, string email, string telefone, Guid userId)
    {
        AtualizarInformacoes(nome, email, telefone, userId);
    }   

    public void Atualizar(string nome, string email, string telefone, Guid userId)
    => AtualizarInformacoes(nome, email, telefone, userId);

    
    private void AtualizarInformacoes(string nome, string email, string telefone, Guid userId)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        UserId = userId;
        ValidarAluno();
    }
    public void AssociarUser(Guid userId) => UserId = userId;

    public override bool IsValid()
    {
        try
        {
            ValidarAluno();
            return true;
        }
        catch
        {
            return false;
        }
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
  
    public void AssociarMatricula(Guid cursoId)
    {
        if (_matriculas.Any(m => m.CursoId == cursoId))
            throw new DomainException("O aluno já está matriculado neste curso.");
        
        var matricula = new Matricula(Id, cursoId, DateTime.Now, true);
        _matriculas.Add(matricula);

    }
    
    public void AssociarCertificado(Guid certificadoId)
    {
        if (_certificados.Any(c => c.Id == certificadoId))
            throw new DomainException("Certificado já associado ao aluno");

        var certificado = new Certificado { Id = certificadoId };
        _certificados.Add(certificado);
    }
    
}