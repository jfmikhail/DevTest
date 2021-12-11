using DeveloperTest.Application.Contracts;
using FluentValidation;
using System;

namespace DeveloperTest.Application.Validators
{
    public class CreateJobCommandValidator : Validator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(command => command).NotNull();
            RuleFor(command => command.Engineer).NotNull().NotEmpty();
            RuleFor(command => command.When).GreaterThan(DateTime.Today)
                .WithMessage("Date cannot be in the past");
            RuleFor(command => command.CustomerId).GreaterThan(0).WithMessage("Assigned customer is required");
        }
    }
}
