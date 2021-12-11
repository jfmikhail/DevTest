using System;

namespace DeveloperTest.Application.Contracts
{
    public record CustomerJobDto(int JobId, string Engineer, DateTime When, string CustomerName);
}
