import { JobModel } from './job.model';

export interface CustomerModel {
    customerId: number;
    name: string;
    type: string;
    jobs: JobModel[];
}


  