import { Component, OnInit } from '@angular/core';
import { CustomerModel } from '../models/customer.model';
import { CustomerService } from '../services/customer.service';
import { CustomerType } from '../models/enums/customerType.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  public customers: CustomerModel[] = [];
  public customerTypes: CustomerType[] = [];
  public newCustomer: CustomerModel = {
    customerId: undefined,
    name: undefined,
    type: undefined,
    jobs: undefined
  };

  constructor(
    private customerService: CustomerService
  ) { }

  ngOnInit() {
    this.customerService.getCustomers()
      .subscribe(customersResponse => this.customers = customersResponse.data.customers);

    this.customerTypes = CustomerType.getAllCustomerTypes();
  }

  public createCustomer(form: NgForm): void{
    if(form.invalid || !this.newCustomer){
      alert('form is not valid');
    } else {
      this.customerService.createCustomer(this.newCustomer)
        .subscribe(addedCustomerResult=>{
          this.newCustomer.customerId = addedCustomerResult.data.customerId;
          this.customers.push(this.newCustomer);
        } ,
          error => alert(error.message))
    } 
  }

}
