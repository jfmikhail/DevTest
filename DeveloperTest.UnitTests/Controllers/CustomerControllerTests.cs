using AutoFixture;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Controllers;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperTest.UnitTests.Controllers
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICreateCustomerApplicationService> _mockCreateCustomerApplicationService;
        private readonly Mock<IGetCustomerApplicationService> _mockGetCustomerApplicationService;
        private readonly Mock<IGetAllCustomersApplicationService> _mockGetAllCustomersApplicationService;

        private readonly IFixture _fixture;
        private readonly CustomerController _customerController;

        public CustomerControllerTests()
        {
            _mockCreateCustomerApplicationService = new Mock<ICreateCustomerApplicationService>();
            _mockGetCustomerApplicationService = new Mock<IGetCustomerApplicationService>();
            _mockGetAllCustomersApplicationService = new Mock<IGetAllCustomersApplicationService>();

            _fixture = new Fixture();
            _customerController = new CustomerController(_mockCreateCustomerApplicationService.Object,
                _mockGetCustomerApplicationService.Object,
                _mockGetAllCustomersApplicationService.Object);
        }

        [Fact]
        public async Task GetAllCustomers_Success()
        {
            //Arrange
            var mockGetAllCustomersResponse = _fixture.Create<GetAllCustomersResponse>();
            var mockAppServiceResponse = ApplicationResult<GetAllCustomersResponse>.Success(mockGetAllCustomersResponse);

            _mockGetAllCustomersApplicationService.Setup(x => x.ExecuteAsync(It.IsAny<GetAllCustomersQuery>()))
                .ReturnsAsync(mockAppServiceResponse);

            //Act
            var result = await _customerController.GetAsync().ConfigureAwait(false);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customers = Assert.IsType<ApplicationResult<GetAllCustomersResponse>>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(customers, mockAppServiceResponse);
        }

        [Fact]
        public async Task GetCustomer_Success()
        {
            //Arrange
            var mockGetCustomerResponse = _fixture.Create<GetCustomerResponse>();
            var mockAppServiceResponse = ApplicationResult<GetCustomerResponse>.Success(mockGetCustomerResponse);

            _mockGetCustomerApplicationService.Setup(x => x.ExecuteAsync(It.IsAny<GetCustomerQuery>()))
                .ReturnsAsync(mockAppServiceResponse);

            //Act
            var result = await _customerController.GetAsync(1).ConfigureAwait(false);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customer = Assert.IsType<ApplicationResult<GetCustomerResponse>>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(customer, mockAppServiceResponse);
        }

        [Fact]
        public async Task CreateCustomer_Success()
        {
            //Arrange
            var mockCreateCustomerResponse = _fixture.Create<CreateCustomerResponse>();
            var mockAppServiceResponse = ApplicationResult<CreateCustomerResponse>.Success(mockCreateCustomerResponse);

            _mockCreateCustomerApplicationService.Setup(x => x.ExecuteAsync(It.IsAny<CreateCustomerCommand>()))
                .ReturnsAsync(mockAppServiceResponse);

            var mockCreateCustomerCommand = new CreateCustomerCommand("John Dao", CustomerType.Large.ToString());

            //Act
            var result = await _customerController.CreateAsync(mockCreateCustomerCommand).ConfigureAwait(false);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customer = Assert.IsType<ApplicationResult<CreateCustomerResponse>>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(customer, mockAppServiceResponse);
        }
    }
}
