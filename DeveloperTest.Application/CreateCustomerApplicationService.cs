using DeveloperTest.Application.Contracts;
using DeveloperTest.Application.Validators;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using System;
using System.Threading.Tasks;

namespace DeveloperTest.Application
{
    public class CreateCustomerApplicationService : ApplicationService<CreateCustomerResponse, CreateCustomerCommand>, ICreateCustomerApplicationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IValidator<CreateCustomerCommand> _validator;

        public CreateCustomerApplicationService(ICustomerRepository customerRepository) : base()
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _validator = new CreateCustomerCommandValidator();
        }

        public override async Task<ApplicationResult<CreateCustomerResponse>> DoExecuteAsync(CreateCustomerCommand command)
        {
            var validationResult = _validator.Validate(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!Enum.TryParse(typeof(CustomerType), command.Type, out var type))
            {
                return BadRequest("CustomerType provided is not supported");
            }

            var customer = new Customer
            {
                Name = command.Name,
                Type = (CustomerType)type
            };

            await _customerRepository.CreateAsync(customer).ConfigureAwait(false);

            return Created(new CreateCustomerResponse(customer.CustomerId));
        }
    }
}
