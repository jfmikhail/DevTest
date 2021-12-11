import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JobModel } from '../models/job.model';
import { ApiResult } from './contracts/apiResult.model';
import { GetJobsResponse } from './contracts/getJobsResponse';
import { CreateJobResponse } from './contracts/createJobResponse';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  constructor(private httpClient: HttpClient) { }

  public GetJobs(): Observable<ApiResult<GetJobsResponse>> {
    return this.httpClient.get<ApiResult<GetJobsResponse>>('http://localhost:63235/job');
  }

  public GetJob(jobId: number): Observable<ApiResult<JobModel>> {
    return this.httpClient.get<ApiResult<JobModel>>(`http://localhost:63235/job/${jobId}`);
  }

  public CreateJob(job: JobModel): Promise<ApiResult<CreateJobResponse>> {
    return this.httpClient.post<ApiResult<CreateJobResponse>>('http://localhost:63235/job', job).toPromise();
  }
}
