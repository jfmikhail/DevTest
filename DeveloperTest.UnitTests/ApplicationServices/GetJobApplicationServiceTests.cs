using AutoFixture;
using DeveloperTest.Application;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperTest.UnitTests.ApplicationServices
{
    public class GetJobApplicationServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IJobRepository> _mockJobRepository;
        private readonly IFixture _fixture;
        private readonly IGetJobApplicationService _getJobApplicationService;
        public GetJobApplicationServiceTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockJobRepository = new Mock<IJobRepository>();
            _fixture = new Fixture();

            _getJobApplicationService = new GetJobApplicationService(_mockCustomerRepository.Object,
                _mockJobRepository.Object);
        }

        [Fact]
        public async Task GetJob_Success()
        {
            //Arrange
            var getJobQuery = new GetJobQuery(1);
            var mockJob = _fixture.Create<Job>();
            _mockJobRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(mockJob);

            var mockCustomer = _fixture.Create<Customer>();
            _mockCustomerRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(mockCustomer);

            //Act
            var result = await _getJobApplicationService.ExecuteAsync(getJobQuery);

            //Assert
            Assert.IsType<ApplicationResult<GetJobResponse>>(result);
            Assert.True(result.Succeeded);
            Assert.Equal(StatusCode.Success, result.GetStatusCode());
            _mockCustomerRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.AtLeastOnce);
            _mockJobRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task GetJob_Failure_InvalidJobId()
        {
            //Arrange
            var getJobQuery = new GetJobQuery(1);
            Job mockJob = null;
            _mockJobRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(mockJob);

            Customer mockCustomer = null;
            _mockCustomerRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(mockCustomer);

            //Act
            var result = await _getJobApplicationService.ExecuteAsync(getJobQuery);

            //Assert
            Assert.IsType<ApplicationResult<GetJobResponse>>(result);
            Assert.True(!result.Succeeded);
            Assert.Equal(StatusCode.NotFound, result.GetStatusCode());
            _mockJobRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.AtLeastOnce);
            _mockCustomerRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async Task GetJob_Success_NullCustomer()
        {
            //Arrange
            var getJobQuery = new GetJobQuery(1);
            Job mockJob = _fixture.Create<Job>();
            _mockJobRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(mockJob);

            //Act
            var result = await _getJobApplicationService.ExecuteAsync(getJobQuery);

            //Assert
            Assert.IsType<ApplicationResult<GetJobResponse>>(result);
            Assert.True(result.Succeeded);
            Assert.Equal(StatusCode.Success, result.GetStatusCode());
            Assert.True(result.Data.Customer.Name == "Unknown");
            _mockCustomerRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.AtLeastOnce);
            _mockJobRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.AtLeastOnce);
        }
    }
}
