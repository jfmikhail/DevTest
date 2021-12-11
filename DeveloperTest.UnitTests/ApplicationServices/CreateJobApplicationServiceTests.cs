using AutoFixture;
using DeveloperTest.Application;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperTest.UnitTests.ApplicationServices
{
    public class CreateJobApplicationServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IJobRepository> _mockJobRepository;
        private readonly IFixture _fixture;
        private readonly ICreateJobApplicationService _createJobApplicationService;

        public CreateJobApplicationServiceTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockJobRepository = new Mock<IJobRepository>();

            _fixture = new Fixture();

            _createJobApplicationService = new CreateJobApplicationService(_mockJobRepository.Object, _mockCustomerRepository.Object);
        }

        [Fact]
        public async Task CreateCustomer_Success()
        {
            //Arrange
            var createJobCommand = new CreateJobCommand("Ashley", DateTime.Now.AddDays(1), 1);

            var mockCustomer = new Customer
            {
                CustomerId = 1,
                Name = "John Dao",
                Type = CustomerType.Large
            };

            _mockCustomerRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(mockCustomer);

            var mockJob = new Job
            {
                JobId = 1,
                CustomerId = 1,
                Engineer = "Ashley",
                When = DateTime.Now.AddDays(1)
            };
            _mockJobRepository.Setup(x => x.CreateAsync(It.IsAny<Job>())).ReturnsAsync(mockJob);

            //Act
            var result = await _createJobApplicationService.ExecuteAsync(createJobCommand);

            //Assert
            Assert.IsType<ApplicationResult<CreateJobResponse>>(result);
            Assert.True(result.Succeeded);
            Assert.Equal(StatusCode.Created, result.GetStatusCode());
        }

        [Fact]
        public async Task CreateCustomer_Failure_InvalidDate()
        {
            //Arrange
            var createJobCommand = new CreateJobCommand("Ashley", DateTime.Now.AddDays(-1), 1);


            //Act
            var result = await _createJobApplicationService.ExecuteAsync(createJobCommand);

            //Assert
            Assert.IsType<ApplicationResult<CreateJobResponse>>(result);
            Assert.True(!result.Succeeded);
            Assert.Equal(StatusCode.BadRequest, result.GetStatusCode());
            _mockCustomerRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async Task CreateCustomer_Failure_CustomerIdLessThan1()
        {
            //Arrange
            var createJobCommand = new CreateJobCommand("Ashley", DateTime.Now.AddDays(1), 0);

            //Act
            var result = await _createJobApplicationService.ExecuteAsync(createJobCommand);

            //Assert
            Assert.IsType<ApplicationResult<CreateJobResponse>>(result);
            Assert.True(!result.Succeeded);
            Assert.Equal(StatusCode.BadRequest, result.GetStatusCode());
            _mockCustomerRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async Task CreateCustomer_Failure_InvalidCustomer()
        {
            //Arrange
            var createJobCommand = new CreateJobCommand("Ashley", DateTime.Now.AddDays(1), 1);

            Customer customer = null;
            _mockCustomerRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(customer);

            //Act
            var result = await _createJobApplicationService.ExecuteAsync(createJobCommand);

            //Assert
            Assert.IsType<ApplicationResult<CreateJobResponse>>(result);
            Assert.True(!result.Succeeded);
            Assert.Equal(StatusCode.BadRequest, result.GetStatusCode());
            _mockCustomerRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.AtLeastOnce);
            _mockJobRepository.Verify(x => x.CreateAsync(It.IsAny<Job>()), Times.Never);

        }
    }
}
