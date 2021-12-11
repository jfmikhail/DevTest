using DeveloperTest.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeveloperTest.Infrastructure.Storage.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CustomerRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.AddAsync(customer).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return customer;
        }

        public async Task<Customer> GetAsync(int customerId)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Set<Customer>()
                .Where(customer => customer.CustomerId == customerId)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<Customer>> GetAsync(IReadOnlyCollection<int> customerIds)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Set<Customer>()
                .Where(customer => customerIds.Contains(customer.CustomerId)).ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<Customer>> GetAllAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Set<Customer>().ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
