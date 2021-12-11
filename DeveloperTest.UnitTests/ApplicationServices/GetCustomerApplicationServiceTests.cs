using AutoFixture;
using DeveloperTest.Application;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperTest.UnitTests.ApplicationServices
{
    public class GetCustomerApplicationServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IJobRepository> _mockJobRepository;
        private readonly IFixture _fixture;
        private readonly IGetCustomerApplicationService _getCustomerApplicationService;
        public GetCustomerApplicationServiceTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockJobRepository = new Mock<IJobRepository>();
            _fixture = new Fixture();

            _getCustomerApplicationService = new GetCustomerApplicationService(_mockCustomerRepository.Object,
                _mockJobRepository.Object);
        }

        [Fact]
        public async Task GetCustomer_Success()
        {
            //Arrange
            var getCustomerQuery = new GetCustomerQuery(1);
            var mockCustomer = _fixture.Create<Customer>();
            _mockCustomerRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(mockCustomer);

            var mockJobs = _fixture.CreateMany<Job>();
            _mockJobRepository.Setup(x => x.GetCustomerJobsAsync(It.IsAny<int>())).ReturnsAsync(mockJobs.ToList());

            //Act
            var result = await _getCustomerApplicationService.ExecuteAsync(getCustomerQuery);

            //Assert
            Assert.IsType<ApplicationResult<GetCustomerResponse>>(result);
            Assert.True(result.Succeeded);
            Assert.Equal(StatusCode.Success, result.GetStatusCode());
            _mockCustomerRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.AtLeastOnce);
            _mockJobRepository.Verify(x => x.GetCustomerJobsAsync(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task GetCustomer_Failure_InvalidCustomerId()
        {
            //Arrange
            var getCustomerQuery = new GetCustomerQuery(1);
            Customer mockCustomer = null;
            _mockCustomerRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(mockCustomer);

            //Act
            var result = await _getCustomerApplicationService.ExecuteAsync(getCustomerQuery);

            //Assert
            Assert.IsType<ApplicationResult<GetCustomerResponse>>(result);
            Assert.True(!result.Succeeded);
            Assert.Equal(StatusCode.NotFound, result.GetStatusCode());
            _mockCustomerRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.AtLeastOnce);
            _mockJobRepository.Verify(x => x.GetCustomerJobsAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
