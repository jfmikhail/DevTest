using DeveloperTest.Application.Contracts;
using DeveloperTest.Application.Validators;
using System;
using System.Linq;
using Xunit;

namespace DeveloperTest.UnitTests.Validators
{
    public class CreateJobCommandValidatorTests
    {
        private readonly IValidator<CreateJobCommand> _createJobCommandValidator;

        public CreateJobCommandValidatorTests()
        {
            _createJobCommandValidator = new CreateJobCommandValidator();
        }

        [Fact]
        public void CreateJobValidator_Success()
        {
            // Arrange
            var mockCreateJobCommand = new CreateJobCommand("Ashley", DateTime.Today.AddDays(1), 1);

            // Act
            var result = _createJobCommandValidator.Validate(mockCreateJobCommand);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateJobValidator_Failure_InvalidDate()
        {
            // Arrange
            var mockCreateJobCommand = new CreateJobCommand("Ashley", DateTime.Today.AddDays(-1), 1);

            // Act
            var result = _createJobCommandValidator.Validate(mockCreateJobCommand);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 1);
        }

        [Fact]
        public void CreateJobValidator_Failure_InvalidCustomerId()
        {
            // Arrange
            var mockCreateJobCommand = new CreateJobCommand("Ashley", DateTime.Today.AddDays(1), 0);

            // Act
            var result = _createJobCommandValidator.Validate(mockCreateJobCommand);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 1);
        }

        [Fact]
        public void CreateCustomerValidator_Failure_InvalidDateAndCustomerId()
        {
            // Arrange
            var mockCreateJobCommand = new CreateJobCommand("Ashley", DateTime.Today.AddDays(-1), 0);

            // Act
            var result = _createJobCommandValidator.Validate(mockCreateJobCommand);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 2);
        }
    }
}
