using DeveloperTest.Domain.JobAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeveloperTest.Infrastructure.Storage.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public JobRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        public async Task<Job> CreateAsync(Job job)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.AddAsync(job).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return job;
        }

        public async Task<Job> GetAsync(int jobId)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Set<Job>()
                .Where(job => job.JobId == jobId)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<Job>> GetAllAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Set<Job>().ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<Job>> GetCustomerJobsAsync(int customerId)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Set<Job>()
                .Where(job => job.CustomerId == customerId)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
