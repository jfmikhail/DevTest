import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CustomerModel } from '../models/customer.model';
import { Observable } from 'rxjs';
import { ApiResult } from './contracts/apiResult.model';
import { GetCustomersResponse } from './contracts/getCustomersResponse';
import { CreateCustomerResponse } from './contracts/createCustomerResponse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrl;
  constructor(private httpClient: HttpClient) {
    this.apiUrl = environment.apiUrl;
   }

  public getCustomers(): Observable<ApiResult<GetCustomersResponse>> {
    return this.httpClient.get<ApiResult<GetCustomersResponse>>(`${this.apiUrl}/customer`);
  }

  public getCustomer(customerId: number): Observable<ApiResult<CustomerModel>> {
    return this.httpClient.get<ApiResult<CustomerModel>>(`${this.apiUrl}/customer/${customerId}`);
  }

  public createCustomer(customer: CustomerModel): Observable<ApiResult<CreateCustomerResponse>> {
    return this.httpClient.post<ApiResult<CreateCustomerResponse>>(`${this.apiUrl}/customer`, customer);
  }
}
