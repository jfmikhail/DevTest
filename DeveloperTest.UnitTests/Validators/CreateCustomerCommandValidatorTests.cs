using DeveloperTest.Application.Contracts;
using DeveloperTest.Application.Validators;
using DeveloperTest.Domain.CustomerAggregate;
using System.Linq;
using Xunit;

namespace DeveloperTest.UnitTests.Validators
{
    public class CreateCustomerCommandValidatorTests
    {
        private readonly IValidator<CreateCustomerCommand> _createCustomerCommandValidator;

        public CreateCustomerCommandValidatorTests()
        {
            _createCustomerCommandValidator = new CreateCustomerCommandValidator();
        }

        [Fact]
        public void CreateCustomerValidator_Success()
        {
            // Arrange
            var mockCreateCustomerCommand = new CreateCustomerCommand("John Dao", CustomerType.Large.ToString());

            // Act
            var result = _createCustomerCommandValidator.Validate(mockCreateCustomerCommand);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateCustomerValidator_Failure_InvalidCustomerName()
        {
            // Arrange
            var mockCreateCustomerCommand = new CreateCustomerCommand("John", CustomerType.Large.ToString());

            // Act
            var result = _createCustomerCommandValidator.Validate(mockCreateCustomerCommand);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 1);
        }

        [Fact]
        public void CreateCustomerValidator_Failure_InvalidCustomerType()
        {
            // Arrange
            var mockCreateCustomerCommand = new CreateCustomerCommand("John Dao", "Wrong Value");
            
            // Act
            var result = _createCustomerCommandValidator.Validate(mockCreateCustomerCommand);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 1);
        }

        [Fact]
        public void CreateCustomerValidator_Failure_InvalidCustomerNameAndType()
        {
            // Arrange
            var mockCreateCustomerCommand = new CreateCustomerCommand("John", "Wrong Value");
            
            // Act
            var result = _createCustomerCommandValidator.Validate(mockCreateCustomerCommand);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 2);
        }
    }
}
