using AutoFixture;
using DeveloperTest.Application;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperTest.UnitTests.ApplicationServices
{
    public class GetAllJobsApplicationServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IJobRepository> _mockJobRepository;
        private readonly IFixture _fixture;
        private readonly IGetAllJobsApplicationService _getAllJobsApplicationService;
        public GetAllJobsApplicationServiceTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockJobRepository = new Mock<IJobRepository>();
            _fixture = new Fixture();

            _getAllJobsApplicationService = new GetAllJobsApplicationService(_mockCustomerRepository.Object
                , _mockJobRepository.Object);
        }

        [Fact]
        public async Task GetJobs_Success()
        {
            //Arrange
            var getAllJobsQuery = new GetAllJobsQuery();

            var mockJobs = _fixture.CreateMany<Job>();
            _mockJobRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(mockJobs.ToList());

            var mockCustomers = _fixture.CreateMany<Customer>();
            _mockCustomerRepository.Setup(x => x.GetAsync(It.IsAny<IReadOnlyCollection<int>>()))
                .ReturnsAsync(mockCustomers.ToList());

            //Act
            var result = await _getAllJobsApplicationService.ExecuteAsync(getAllJobsQuery);

            //Assert
            Assert.IsType<ApplicationResult<GetAllJobsResponse>>(result);
            Assert.True(result.Succeeded);
            Assert.Equal(StatusCode.Success, result.GetStatusCode());
            Assert.NotNull(result.Data.Jobs);
            Assert.Equal(mockJobs.Count(), result.Data.Jobs.Count);
            _mockJobRepository.Verify(x => x.GetAllAsync(), Times.AtLeastOnce);
            _mockCustomerRepository.Verify(x => x.GetAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.AtLeastOnce);
        }
    }
}
