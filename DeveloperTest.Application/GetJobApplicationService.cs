using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using System;
using System.Threading.Tasks;

namespace DeveloperTest.Application
{
    public class GetJobApplicationService : ApplicationService<GetJobResponse, GetJobQuery>, IGetJobApplicationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJobRepository _jobRepository;

        public GetJobApplicationService(ICustomerRepository customerRepository,
            IJobRepository jobRepository) : base()
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
        }

        public override async Task<ApplicationResult<GetJobResponse>> DoExecuteAsync(GetJobQuery query)
        {
            var job = await _jobRepository.GetAsync(query.JobId).ConfigureAwait(false);

            if (job is null)
            {
                return NotFound($"Job with id: {query.JobId} is not found.");
            }

            var customer = job.CustomerId is null ? null :
                await _customerRepository.GetAsync(job.CustomerId.Value).ConfigureAwait(false);

            if (customer is null)
            {
                return Success(new GetJobResponse(job.JobId,
                                              job.Engineer,
                                              job.When,
                                              new CustomerDto(-1, "Unknown", null)));
            }

            return Success(new GetJobResponse(job.JobId,
                                              job.Engineer,
                                              job.When,
                                              new CustomerDto(customer.CustomerId,
                                                customer.Name,
                                                customer.Type.ToString())));
        }
    }
}
