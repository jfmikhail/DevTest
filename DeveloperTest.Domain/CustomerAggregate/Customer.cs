namespace DeveloperTest.Domain.CustomerAggregate
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public CustomerType Type { get; set; }
    }
}
