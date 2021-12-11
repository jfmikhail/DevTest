using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain.CustomerAggregate;
using FluentValidation;
using System;

namespace DeveloperTest.Application.Validators
{
    public class CreateCustomerCommandValidator : Validator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(command => command).NotNull();
            RuleFor(command => command.Name).NotNull().NotEmpty().MinimumLength(5)
                .WithMessage("Customer name is required and must have a minimum length of 5 characters");
            RuleFor(command => command.Type).NotNull().NotEmpty().Must(BeValidCustomerType)
                .WithMessage("Customer type should be either \"Large\" or \"Small\".");
        }

        private static bool BeValidCustomerType(string type)
        {
            return Enum.TryParse(typeof(CustomerType), type, out var _);
        }
    }
}
