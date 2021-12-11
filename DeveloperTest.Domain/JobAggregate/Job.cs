using System;

namespace DeveloperTest.Domain.JobAggregate
{
    public class Job
    {
        public int JobId { get; set; }

        public string Engineer { get; set; }

        public DateTime When { get; set; }

        public int? CustomerId { get; set; }
    }
}
