﻿using MediatR;
using FluentValidation.Results;
namespace EadFacil.Core.Messages;

public abstract class Command :Message, IRequest<bool>
{
    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; private set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }
    public virtual bool IsValid()
    {
       throw new NotImplementedException();
    }
}