using EadFacil.Core.DomainObjects;

namespace EadFacil.BC.Conteudo.Domain.Entities;

public class Aula : Entity
{
    public string Conteudo { get; private set; }
    public string Titulo { get; private set; }
    public string? Material { get; private set; }
    public Guid CursoId { get; private set; }
    
    protected Aula() { } // Construtor protegido para EF Core
    
    public Aula(string titulo, string conteudo, Guid cursoId, string? material = null)
    {
        CursoId = cursoId; 
        Titulo = titulo;
        Conteudo = conteudo;
        Material = material;
        ValidarAula();
    }
    
    public void Atualizar(string titulo, string conteudo, string? material = null)
    {
        Titulo = titulo;
        Conteudo = conteudo;
        Material = material;
        ValidarAula();
    }
    private void ValidarAula()
    {
        if (string.IsNullOrWhiteSpace(Titulo))
            throw new DomainException("O título da aula é obrigatório");

        if (string.IsNullOrWhiteSpace(Conteudo))
            throw new DomainException("O conteúdo da aula é obrigatório");
  
    }
    
    public override bool IsValid()
    {
        try
        {
            ValidarAula();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void AssociarCurso(Guid id)
    {
        CursoId = id;
    }
}