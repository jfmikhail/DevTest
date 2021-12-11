using DeveloperTest.Application.Contracts;
using DeveloperTest.Application.Validators;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using System;
using System.Threading.Tasks;

namespace DeveloperTest.Application
{
    public class CreateJobApplicationService : ApplicationService<CreateJobResponse, CreateJobCommand>, ICreateJobApplicationService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IValidator<CreateJobCommand> _validator;
        public CreateJobApplicationService(IJobRepository jobRepository,
            ICustomerRepository customerRepository) : base()
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _validator = new CreateJobCommandValidator();
        }

        public override async Task<ApplicationResult<CreateJobResponse>> DoExecuteAsync(CreateJobCommand command)
        {
            var validationResult = _validator.Validate(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var customer = await _customerRepository.GetAsync(command.CustomerId).ConfigureAwait(false);

            if (customer is null)
            {
                return BadRequest($"Invalid {nameof(customer)}");
            }

            var job = new Job()
            {
                CustomerId = command.CustomerId,
                Engineer = command.Engineer,
                When = command.When
            };

            await _jobRepository.CreateAsync(job).ConfigureAwait(false);

            return Created(new CreateJobResponse(job.JobId));
        }
    }
}
