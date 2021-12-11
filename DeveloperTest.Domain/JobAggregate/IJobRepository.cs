using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeveloperTest.Domain.JobAggregate
{
    public interface IJobRepository
    {
        Task<Job> CreateAsync(Job job);

        Task<Job> GetAsync(int jobId);

        Task<IReadOnlyCollection<Job>> GetAllAsync();

        Task<IReadOnlyCollection<Job>> GetCustomerJobsAsync(int customerId);
    }
}
