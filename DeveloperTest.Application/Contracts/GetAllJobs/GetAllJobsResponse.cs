using System.Collections.Generic;

namespace DeveloperTest.Application.Contracts
{
    public record GetAllJobsResponse(IReadOnlyCollection<CustomerJobDto> Jobs);
}
