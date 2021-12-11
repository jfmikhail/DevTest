export class CustomerType{
    public static large = new CustomerType("Large", 1);
    public static small = new CustomerType("Small", 2);

    constructor(private name, private key){}

    public static getAllCustomerTypes(){
        return [CustomerType.large, CustomerType.small];
    }
}