using System.Collections.Generic;

namespace DeveloperTest.Application.Contracts
{
    public record GetAllCustomersResponse(IReadOnlyCollection<CustomerDto> Customers);
}
