using AutoFixture;
using DeveloperTest.Application;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperTest.UnitTests.ApplicationServices
{
    public class CreateCustomerApplicationServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly IFixture _fixture;
        private readonly ICreateCustomerApplicationService _createCustomerApplicationService;
        public CreateCustomerApplicationServiceTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _fixture = new Fixture();

            _createCustomerApplicationService = new CreateCustomerApplicationService(_mockCustomerRepository.Object);
        }

        [Fact]
        public async Task CreateCustomer_Success()
        {
            //Arrange
            var createCustomerCommand = new CreateCustomerCommand("John Dao", CustomerType.Large.ToString());

            var mockCustomer = new Customer
            {
                CustomerId = 1,
                Name = "John Dao",
                Type = CustomerType.Large
            };

            _mockCustomerRepository.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(mockCustomer);

            //Act
            var result = await _createCustomerApplicationService.ExecuteAsync(createCustomerCommand);

            //Assert
            Assert.IsType<ApplicationResult<CreateCustomerResponse>>(result);
            Assert.True(result.Succeeded);
            Assert.Equal(StatusCode.Created, result.GetStatusCode());
        }

        [Fact]
        public async Task CreateCustomer_Failure_InvalidCustomerName()
        {
            //Arrange
            var createCustomerCommand = new CreateCustomerCommand("John", CustomerType.Large.ToString());

            //Act
            var result = await _createCustomerApplicationService.ExecuteAsync(createCustomerCommand);

            //Assert
            Assert.IsType<ApplicationResult<CreateCustomerResponse>>(result);
            Assert.True(!result.Succeeded);
            _mockCustomerRepository.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public async Task CreateCustomer_Failure_InvalidCustomerType()
        {
            //Arrange
            var createCustomerCommand = new CreateCustomerCommand("John Dao", "Wrong Value");

            //Act
            var result = await _createCustomerApplicationService.ExecuteAsync(createCustomerCommand);

            //Assert
            Assert.IsType<ApplicationResult<CreateCustomerResponse>>(result);
            Assert.True(!result.Succeeded);
            _mockCustomerRepository.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Never);
        }
    }
}
