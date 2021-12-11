using AutoFixture;
using DeveloperTest.Application;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperTest.UnitTests.ApplicationServices
{
    public class GetAllCustomersApplicationServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly IFixture _fixture;
        private readonly IGetAllCustomersApplicationService _getAllCustomersApplicationService;
        public GetAllCustomersApplicationServiceTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _fixture = new Fixture();

            _getAllCustomersApplicationService = new GetAllCustomersApplicationService(_mockCustomerRepository.Object);
        }

        [Fact]
        public async Task GetCustomers_Success()
        {
            //Arrange
            var getAllCustomersQuery = new GetAllCustomersQuery();
            var mockCustomers = _fixture.CreateMany<Customer>();
            _mockCustomerRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(mockCustomers.ToList());

            //Act
            var result = await _getAllCustomersApplicationService.ExecuteAsync(getAllCustomersQuery);

            //Assert
            Assert.IsType<ApplicationResult<GetAllCustomersResponse>>(result);
            Assert.True(result.Succeeded);
            Assert.Equal(StatusCode.Success, result.GetStatusCode());
            _mockCustomerRepository.Verify(x => x.GetAllAsync(), Times.AtLeastOnce);
        }
    }
}
