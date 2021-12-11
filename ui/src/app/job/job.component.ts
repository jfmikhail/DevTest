import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EngineerService } from '../services/engineer.service';
import { JobService } from '../services/job.service';
import { JobModel } from '../models/job.model';
import { CustomerService } from '../services/customer.service';
import { CustomerModel } from '../models/customer.model';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss']
})
export class JobComponent implements OnInit {

  public engineers: string[] = [];
  public customers: CustomerModel[] = [];
  public jobs: JobModel[] = [];

  public newJob: JobModel = {
    jobId: undefined,
    engineer: undefined,
    when: undefined,
    customerName:undefined,
    customerId: undefined,
    customer: undefined
  };

  constructor(
    private engineerService: EngineerService,
    private jobService: JobService,
    private customerService: CustomerService) { }

  ngOnInit() {
    this.engineerService.GetEngineers().subscribe(engineers => this.engineers = engineers);
    this.jobService.GetJobs().subscribe(jobsResponse => this.jobs = jobsResponse.data.jobs);
    this.customerService.getCustomers().subscribe(customersResponse => this.customers = customersResponse.data.customers);
  }

  public createJob(form: NgForm): void {
    if (form.invalid) {
      alert('form is not valid');
    } else {
      this.jobService.CreateJob(this.newJob).then(jobResponse => {
        
        this.newJob.jobId = jobResponse.data.jobId;
        this.newJob.customerName = this.customers
          .filter(customer => customer.customerId == this.newJob.customerId)[0].name;

        this.jobs.push(this.newJob);
      },errorResult => alert(errorResult.error.errors.join(" - ")));
    }
  }

}
