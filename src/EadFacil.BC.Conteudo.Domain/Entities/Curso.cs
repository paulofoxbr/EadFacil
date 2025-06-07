using EadFacil.BC.Conteudo.Domain.Event;
using EadFacil.BC.Conteudo.Domain.Validadions;
using EadFacil.BC.Conteudo.Domain.ValueObjects;
using EadFacil.Core.DomainObjects;
using EadFacil.Core.Messages;
using EadFacil.Core.Messages.CommonMessages.DomainEvents;
using EadFacil.Core.Messages.CommonMessages.Notifications;

namespace EadFacil.BC.Conteudo.Domain.Entities;

public class Curso : Entity, IAggregateRoot
{
    public string Titulo { get; private set; }
    public ConteudoProgramatico ConteudoProgramatico { get; private set; }

    private readonly List<Aula> _aulas;
    public IReadOnlyCollection<Aula> Aulas => _aulas;
    
    // Navegação para a aula associada ao curso
    public Aula Aula {get; private set; }

    // Construtor protegido para EF Core
    protected Curso()
    {
        _aulas = new List<Aula>();
    } 
    
    public Curso(string titulo, ConteudoProgramatico conteudo)
    {
        Titulo = titulo;
        ConteudoProgramatico = conteudo;
        ValidarCurso();
    }
    public void Atualizar(string titulo, ConteudoProgramatico conteudoProgramatico)
    {
        Titulo = titulo;
        this.ConteudoProgramatico = conteudoProgramatico;
        ValidarCurso();
    }
    private void ValidarCurso()
    {
        if (string.IsNullOrWhiteSpace(Titulo))
            throw new DomainException("O título do curso é obrigatório");

        if (ConteudoProgramatico == null)
            throw new DomainException("O conteúdo programático do curso é obrigatório");
    }
/*
    public bool ValidarComFluentValidation()
    {
        var validator = new CursoValidator();
        var validationResult = validator.Validate(this);
        var meuId = Id;
        
        foreach (var error in validationResult.Errors)
        {
            var errorMessage = error.ErrorMessage;
           AddEvent(new CursoValidacoEvent(Id, errorMessage));
        }
        
        return validationResult.IsValid;
    }
*/
    
    
    public override bool IsValid()
    {
        // Validação usando FluentValidation
       // return  ValidarComFluentValidation();
        
        // Validação básica do curso
        ValidarCurso();
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
        _aulas.Add(aula);
    }
    public void RemoverAula(Aula aula)
    {
        if (aula == null)
            throw new DomainException("A aula não pode ser nula");
        _aulas.Remove(aula);
    }
    public void LimparAulas()
    {
        _aulas.Clear();
    }
    public void AtualizarAula(Aula aula)
    {
        if (aula == null)
            throw new DomainException("A aula não pode ser nula");

        if (aula.CursoId != this.Id)
            throw new DomainException("A aula não pertence a este curso");
        
        var aulaExistente = _aulas.FirstOrDefault(a => a.Id == aula.Id);
        if (aulaExistente == null)
            throw new DomainException("A aula não foi encontrada no curso");

        _aulas.Remove(aulaExistente);
        _aulas.Add(aula);
        
    }
}