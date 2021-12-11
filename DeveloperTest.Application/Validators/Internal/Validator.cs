using FluentValidation;
using System.Linq;

namespace DeveloperTest.Application.Validators
{
    public abstract class Validator<T> : AbstractValidator<T>, IValidator<T>
    {
        /// <summary>
		/// Validates the specified instance
		/// </summary>
		/// <param name="instance">The object to validate</param>
		/// <returns>A ValidationResult object containing any validation failures</returns>
        public new ValidationResult Validate(T instance)
        {
            var fluentValidationResult = base.Validate(instance);
            return new ValidationResult(fluentValidationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}
