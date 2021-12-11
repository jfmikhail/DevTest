import { CustomerModel } from 'src/app/models/customer.model';

export interface GetCustomersResponse{
    customers:CustomerModel[];
}