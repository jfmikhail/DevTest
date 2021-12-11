import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CustomerModel } from '../models/customer.model';
import { Observable } from 'rxjs';
import { ApiResult } from './contracts/apiResult.model';
import { GetCustomersResponse } from './contracts/getCustomersResponse';
import { CreateCustomerResponse } from './contracts/createCustomerResponse';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private httpClient: HttpClient) { }

  public getCustomers(): Observable<ApiResult<GetCustomersResponse>> {
    return this.httpClient.get<ApiResult<GetCustomersResponse>>('http://localhost:63235/customer');
  }

  public getCustomer(customerId: number): Observable<ApiResult<CustomerModel>> {
    return this.httpClient.get<ApiResult<CustomerModel>>(`http://localhost:63235/customer/${customerId}`);
  }

  public createCustomer(customer: CustomerModel): Observable<ApiResult<CreateCustomerResponse>> {
    return this.httpClient.post<ApiResult<CreateCustomerResponse>>('http://localhost:63235/customer', customer);
  }
}
