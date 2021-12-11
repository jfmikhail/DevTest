using DeveloperTest.Application.Contracts;
using DeveloperTest.Domain;
using DeveloperTest.Domain.CustomerAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeveloperTest.Application
{
    public class GetAllCustomersApplicationService : ApplicationService<GetAllCustomersResponse, GetAllCustomersQuery>, IGetAllCustomersApplicationService
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersApplicationService(ICustomerRepository customerRepository) : base()
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public override async Task<ApplicationResult<GetAllCustomersResponse>> DoExecuteAsync(GetAllCustomersQuery query)
        {
            var customers = await _customerRepository.GetAllAsync().ConfigureAwait(false);

            return Success(new GetAllCustomersResponse(
                customers.Select(c =>
                    new CustomerDto(c.CustomerId, c.Name, c.Type.ToString())).ToList()
                ));
        }
    }
}
