using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperTest.Application.Validators
{
    public class ValidationResult
    {
        /// <summary>
        /// Creates a new ValidationResult from a collection of failures
        /// </summary>
        /// <param name="failures">List of <see cref="errors"/> which is later available through <see cref="Errors"/>. This list get's copied.</param>
        /// <remarks>
        /// Every caller is responsible for not adding <c>null</c> to the list.
        /// </remarks>
        public ValidationResult(IEnumerable<string> failures)
        {
            Errors = failures.Where(failure => failure != null);
        }

        /// <summary>
        /// Whether validation succeeded
        /// </summary>
        public virtual bool IsValid => !Errors.Any();

        /// <summary>
        /// A collection of errors
        /// </summary>
        public IEnumerable<string> Errors { get; }

        /// <summary>
        /// Generates a string representation of the error messages separated by new lines.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(Environment.NewLine);
        }

        /// <summary>
        /// Generates a string representation of the error messages separated by the specified character.
        /// </summary>
        /// <param name="separator">The character to separate the error messages.</param>
        /// <returns></returns>
        public string ToString(string separator)
        {
            return string.Join(separator, Errors.Select(failure => failure));
        }
    }
}
