using MediatR;
using FluentValidation.Results;
namespace EadFacil.Core.Messages;

public abstract class Command :Message, IRequest<bool>
{
    public DateTime CreatedAt { get; private set; }
    public ValidationResult ValidationResult { get; private set; }

    protected Command()
    {
        CreatedAt = DateTime.UtcNow;
        ValidationResult = new ValidationResult();
    }
    public virtual bool IsValid()
    {
       throw new NotImplementedException();
    }
}