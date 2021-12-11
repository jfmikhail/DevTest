using DeveloperTest.Domain.CustomerAggregate;
using DeveloperTest.Domain.JobAggregate;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeveloperTest.Infrastructure.Storage
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasKey(x => x.CustomerId);

            modelBuilder.Entity<Customer>()
                .Property(x => x.CustomerId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Job>()
                .HasKey(x => x.JobId);

            modelBuilder.Entity<Job>()
                .Property(x => x.JobId)
                .ValueGeneratedOnAdd();

            //Seeding data

            modelBuilder.Entity<Customer>()
                .HasData(new Customer
                {
                    CustomerId = 1,
                    Name = "Iain Mckenna",
                    Type = CustomerType.Large
                });

            modelBuilder.Entity<Job>()
                .HasData(new Job
                {
                    JobId = 1,
                    Engineer = "Test",
                    When = DateTime.Now
                });
        }
    }
}
