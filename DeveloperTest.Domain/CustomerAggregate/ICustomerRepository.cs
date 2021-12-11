using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeveloperTest.Domain.CustomerAggregate
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateAsync(Customer customer);

        Task<Customer> GetAsync(int customerId);

        Task<IReadOnlyCollection<Customer>> GetAsync(IReadOnlyCollection<int> customerIds);

        Task<IReadOnlyCollection<Customer>> GetAllAsync();
    }
}
