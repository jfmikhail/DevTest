using AutoFixture;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Controllers;
using DeveloperTest.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperTest.UnitTests.Controllers
{
    public class JobControllerTests
    {
        private readonly Mock<ICreateJobApplicationService> _mockCreateJobApplicationService;
        private readonly Mock<IGetJobApplicationService> _mockGetJobApplicationService;
        private readonly Mock<IGetAllJobsApplicationService> _mockGetAllJobsApplicationService;

        private readonly IFixture _fixture;
        private readonly JobController _jobController;

        public JobControllerTests()
        {
            _mockCreateJobApplicationService = new Mock<ICreateJobApplicationService>();
            _mockGetJobApplicationService = new Mock<IGetJobApplicationService>();
            _mockGetAllJobsApplicationService = new Mock<IGetAllJobsApplicationService>();

            _fixture = new Fixture();
            _jobController = new JobController(_mockGetAllJobsApplicationService.Object,
                _mockGetJobApplicationService.Object,
                _mockCreateJobApplicationService.Object);
        }

        [Fact]
        public async Task GetAllJobs_Success()
        {
            //Arrange
            var mockGetAllJobsResponse = _fixture.Create<GetAllJobsResponse>();
            var mockAppServiceResponse = ApplicationResult<GetAllJobsResponse>.Success(mockGetAllJobsResponse);

            _mockGetAllJobsApplicationService.Setup(x => x.ExecuteAsync(It.IsAny<GetAllJobsQuery>()))
                .ReturnsAsync(mockAppServiceResponse);

            //Act
            var result = await _jobController.GetAsync().ConfigureAwait(false);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var jobs = Assert.IsType<ApplicationResult<GetAllJobsResponse>>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(jobs, mockAppServiceResponse);
        }

        [Fact]
        public async Task GetJob_Success()
        {
            //Arrange
            var mockGetJobResponse = _fixture.Create<GetJobResponse>();
            var mockAppServiceResponse = ApplicationResult<GetJobResponse>.Success(mockGetJobResponse);

            _mockGetJobApplicationService.Setup(x => x.ExecuteAsync(It.IsAny<GetJobQuery>()))
                .ReturnsAsync(mockAppServiceResponse);

            //Act
            var result = await _jobController.GetAsync(1).ConfigureAwait(false);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var job = Assert.IsType<ApplicationResult<GetJobResponse>>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(job, mockAppServiceResponse);
        }

        [Fact]
        public async Task CreateJob_Success()
        {
            //Arrange
            var mockCreateJobResponse = _fixture.Create<CreateJobResponse>();
            var mockAppServiceResponse = ApplicationResult<CreateJobResponse>.Success(mockCreateJobResponse);

            _mockCreateJobApplicationService.Setup(x => x.ExecuteAsync(It.IsAny<CreateJobCommand>()))
                .ReturnsAsync(mockAppServiceResponse);

            var mockCreateJobCommand = new CreateJobCommand("Ashley", DateTime.Today.AddDays(1), 1);

            //Act
            var result = await _jobController.CreateAsync(mockCreateJobCommand).ConfigureAwait(false);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var job = Assert.IsType<ApplicationResult<CreateJobResponse>>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(job, mockAppServiceResponse);
        }
    }
}
