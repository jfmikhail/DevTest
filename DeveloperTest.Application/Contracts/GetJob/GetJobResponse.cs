using System;

namespace DeveloperTest.Application.Contracts
{
    public record GetJobResponse(int JobId, string Engineer, DateTime When, CustomerDto Customer);
}
