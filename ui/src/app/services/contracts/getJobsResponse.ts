import { JobModel } from 'src/app/models/job.model';

export interface GetJobsResponse{
    jobs:JobModel[];
}