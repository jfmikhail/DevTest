using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;

namespace DeveloperTest.Application
{
    public class GetAllJobsApplicationService : ApplicationService<GetAllJobsResponse, GetAllJobsQuery>, IGetAllJobsApplicationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJobRepository _jobRepository;

        public GetAllJobsApplicationService(ICustomerRepository customerRepository,
            IJobRepository jobRepository) : base()
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
        }

        public override async Task<ApplicationResult<GetAllJobsResponse>> DoExecuteAsync(GetAllJobsQuery query)
        {
            var jobs = await _jobRepository.GetAllAsync().ConfigureAwait(false);

            var customerIds = jobs.Where(j => j.CustomerId != null).Select(j => j.CustomerId.Value).ToList();

            var customers = await _customerRepository.GetAsync(customerIds).ConfigureAwait(false);

            return Success(new GetAllJobsResponse(
                jobs.Select(j=> 
                    new CustomerJobDto(j.JobId, j.Engineer, j.When,
                    customers.FirstOrDefault(c => c.CustomerId == j.CustomerId)?.Name ?? "Unknown")
                ).ToList()));
        }
    }
}
