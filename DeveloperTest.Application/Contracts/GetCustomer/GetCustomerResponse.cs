using System.Collections.Generic;

namespace DeveloperTest.Application.Contracts
{
    public record GetCustomerResponse(int CustomerId, string Name, string Type, IReadOnlyList<JobDto> Jobs);
}
