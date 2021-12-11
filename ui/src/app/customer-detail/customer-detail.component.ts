import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { ActivatedRoute } from '@angular/router';
import { CustomerModel } from '../models/customer.model';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss']
})
export class CustomerDetailComponent implements OnInit {

  private customerId: number;
  public customer: CustomerModel = undefined;

  constructor(
    private route: ActivatedRoute,
    private customerService:CustomerService) { 
      this.customerId = route.snapshot.params.id;
    }

  ngOnInit() {
    this.customerService.getCustomer(this.customerId)
      .subscribe(customerResult => this.customer = customerResult.data);
  }

}
