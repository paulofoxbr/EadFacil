using EadFacil.BC.Conteudo.Domain.Entities;
using FluentValidation;

namespace EadFacil.BC.Conteudo.Domain.Validadions;

public class AulaValidador: AbstractValidator<Aula>
{
    public AulaValidador()
    {
        RuleFor(a => a.Titulo)
            .NotEmpty()
            .WithMessage("O título da aula não pode ser vazio.")
            .MaximumLength(200)
            .WithMessage("O título da aula não pode exceder 200 caracteres.");
        
        RuleFor(a => a.Conteudo)
            .NotEmpty()
            .WithMessage("O conteúdo da aula não pode ser vazio.")
            .MaximumLength(200)
            .WithMessage("O conteúdo da aula não pode exceder 200 caracteres.");
            

    }
    
}