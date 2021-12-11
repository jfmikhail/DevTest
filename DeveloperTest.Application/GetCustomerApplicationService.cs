using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeveloperTest.Application
{
    public class GetCustomerApplicationService : ApplicationService<GetCustomerResponse, GetCustomerQuery>, IGetCustomerApplicationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJobRepository _jobRepository;

        public GetCustomerApplicationService(ICustomerRepository customerRepository,
            IJobRepository jobRepository) : base()
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
        }

        public override async Task<ApplicationResult<GetCustomerResponse>> DoExecuteAsync(GetCustomerQuery query)
        {
            var customer = await _customerRepository.GetAsync(query.CustomerId).ConfigureAwait(false);

            if (customer is null)
            {
                return NotFound($"Customer with Id: {query.CustomerId} is not found.");
            }

            var customerJobs = await _jobRepository.GetCustomerJobsAsync(query.CustomerId).ConfigureAwait(false);

            var jobsList = customerJobs
                .Select(j => new JobDto(j.JobId, j.Engineer, j.When)).ToList();

            return Success(new GetCustomerResponse(customer.CustomerId,
                                                customer.Name,
                                                customer.Type.ToString(),
                                                jobsList));
        }
    }
}
