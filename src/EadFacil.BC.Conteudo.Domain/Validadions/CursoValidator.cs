using EadFacil.BC.Conteudo.Domain.Entities;
using FluentValidation;
using FluentValidation.Validators;

namespace EadFacil.BC.Conteudo.Domain.Validadions;

public class CursoValidator : AbstractValidator<Curso>
{
   public CursoValidator()
   {
      RuleFor(c => c.Titulo)
         .NotEmpty()
         .WithMessage("O título do curso não pode ser vazio.")
         .MaximumLength(200)
         .WithMessage("O título do curso não pode exceder 200 caracteres.");

      RuleFor(c => c.ConteudoProgramatico.Ementa)
         .NotEmpty()
         .WithMessage("A ementa do curso não pode ser vazia.")
         .MaximumLength(1000)
         .WithMessage("A ementa do curso não pode exceder 1000 caracteres.");

      RuleFor(c => c.ConteudoProgramatico.Materiais)
         .MaximumLength(1000)
         .WithMessage("Os materiais do curso não podem exceder 1000 caracteres.");
   }
}