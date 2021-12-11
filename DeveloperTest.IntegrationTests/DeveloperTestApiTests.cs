using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain.CustomerAggregate;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperTest.IntegrationTests
{
    public class DeveloperTestApiTests : IClassFixture<DeveloperTestWebApplicationFactory<Startup>>
    {
        private readonly DeveloperTestWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public DeveloperTestApiTests(DeveloperTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task CreateCustomer_Failure_InvalidCustomerName()
        {
            var createCustomerCommand = new CreateCustomerCommand("fail", CustomerType.Large.ToString());

            var response = await _client.PostAsJsonAsync("customer", createCustomerCommand)
                .ConfigureAwait(false);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateCustomer_Failure_InvalidCustomerType()
        {
            var createCustomerCommand = new CreateCustomerCommand("Martin King", "Wrong Value");

            var response = await _client.PostAsJsonAsync("customer", createCustomerCommand)
                .ConfigureAwait(false);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateCustomer_Success()
        {
            var createCustomerCommand = new CreateCustomerCommand("Martin King", CustomerType.Large.ToString());

            var response = await _client.PostAsJsonAsync("customer", createCustomerCommand)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetCustomers_Success()
        {
            var response = await _client.GetAsync("customer")
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetCustomer_Success()
        {
            var response = await _client.GetAsync("customer/1")
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetJobs_Success()
        {
            var response = await _client.GetAsync("job")
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetJob_Success()
        {
            var response = await _client.GetAsync("job/1")
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateJob_Success()
        {
            var createJobCommand = new CreateJobCommand("Ashley", DateTime.Now.AddDays(1), 1);

            var response = await _client.PostAsJsonAsync("job", createJobCommand)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateJob_Failure_InvalidDate()
        {
            var createJobCommand = new CreateJobCommand("Ashley", DateTime.Now.AddDays(-1), 1);

            var response = await _client.PostAsJsonAsync("job", createJobCommand)
                .ConfigureAwait(false);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateJob_Failure_InvalidCustomerId()
        {
            var createJobCommand = new CreateJobCommand("Ashley", DateTime.Now.AddDays(1), 0);

            var response = await _client.PostAsJsonAsync("job", createJobCommand)
                .ConfigureAwait(false);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

    }
}
