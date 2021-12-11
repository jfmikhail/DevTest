using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using DeveloperTest.Infrastructure.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;

namespace DeveloperTest.IntegrationTests
{
    public class DeveloperTestWebApplicationFactory<TStartup> :
        WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                    db.Database.EnsureCreated();

                    try
                    {
                        InitializeDbForTests(db);
                    }
                    catch
                    {
                    }
                }
            });
        }

        private static void InitializeDbForTests(ApplicationDbContext context)
        {
            context.Set<Customer>().AddRange(GetSeedingCustomers());
            context.Set<Job>().AddRange(GetSeedingJobs());

            context.SaveChanges();
        }

        private static List<Customer> GetSeedingCustomers()
        {
            return new List<Customer>()
            {
                new()
                {
                    CustomerId = 1,
                    Name = "John Dao",
                    Type = CustomerType.Large
                },
                new()
                {
                    CustomerId = 2,
                    Name = "Patrik Goualo",
                    Type = CustomerType.Small
                },
                new()
                {
                    CustomerId = 3,
                    Name = "Andeana Johns",
                    Type = CustomerType.Small
                }
            };
        }

        private static List<Job> GetSeedingJobs()
        {
            return new List<Job>()
            {
                new()
                {
                    JobId = 1,
                    CustomerId = 1,
                    Engineer = "Ashley",
                    When= DateTime.Now.AddMinutes(-3)
                },
                new()
                {
                    JobId = 2,
                    CustomerId = 1,
                    Engineer = "Dave",
                    When= DateTime.Now.AddMinutes(-6)
                },
                new()
                {
                    JobId = 3,
                    CustomerId = 2,
                    Engineer = "Kalina",
                    When= DateTime.Now.AddMinutes(-6)
                }
            };
        }
    }
}
