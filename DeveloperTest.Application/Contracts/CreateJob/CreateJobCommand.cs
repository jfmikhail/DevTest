using System;

namespace DeveloperTest.Application.Contracts
{
    public record CreateJobCommand(string Engineer, DateTime When, int CustomerId);
}
