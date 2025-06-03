using EadFacil.BC.Conteudo.Domain.ValueObjects;
using EadFacil.Core.DomainObjects;

namespace EadFacil.BC.Conteudo.Domain.Entities;

public class Curso : Entity, IAggregateRoot
{
    public string Titulo { get; private set; }
    public ConteudoProgramatico conteudoProgramatico { get; private set; }
    // Coleção de aulas do curso 
    // EF Core irá mapear essa coleção como uma relação 1:N
    public ICollection<Aula> Aulas { get; private set; } = new List<Aula>();
   
    protected Curso() { } // Construtor protegido para EF Core
    
    public Curso(string titulo, ConteudoProgramatico conteudoProgramatico)
    {
        Titulo = titulo;
        this.conteudoProgramatico = conteudoProgramatico;
        ValidarCurso();
    }
    public void Atualizar(string titulo, ConteudoProgramatico conteudoProgramatico)
    {
        Titulo = titulo;
        this.conteudoProgramatico = conteudoProgramatico;
        ValidarCurso();
    }
    private void ValidarCurso()
    {
        if (string.IsNullOrWhiteSpace(Titulo))
            throw new DomainException("O título do curso é obrigatório");

        if (conteudoProgramatico == null)
            throw new DomainException("O conteúdo programático do curso é obrigatório");
    }
    
    public override bool IsValid()
    {
        try
        {
            ValidarCurso();
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public void AdicionarAula(Aula aula)
    {
        if (aula == null)
            throw new DomainException("A aula não pode ser nula");
        aula.AssociarCurso(this.Id);
        Aulas.Add(aula);
    }
    public void RemoverAula(Aula aula)
    {
        if (aula == null)
            throw new DomainException("A aula não pode ser nula");
        Aulas.Remove(aula);
    }
    public void LimparAulas()
    {
        Aulas.Clear();
    }
    public void AtualizarAula(Aula aula, string titulo, string conteudo, string? material = null)
    {
        if (aula == null)
            throw new DomainException("A aula não pode ser nula");
        if (!Aulas.Contains(aula))
            throw new DomainException("A aula não está associada a este curso");
        if (aula.CursoId != this.Id)
            throw new DomainException("A aula não pertence a este curso");
        
        aula.Atualizar(titulo, conteudo, material);
    }
}