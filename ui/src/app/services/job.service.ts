import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JobModel } from '../models/job.model';
import { ApiResult } from './contracts/apiResult.model';
import { GetJobsResponse } from './contracts/getJobsResponse';
import { CreateJobResponse } from './contracts/createJobResponse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  private apiUrl;
  constructor(private httpClient: HttpClient) { 
    this.apiUrl = environment.apiUrl;
  }

  public GetJobs(): Observable<ApiResult<GetJobsResponse>> {
    return this.httpClient.get<ApiResult<GetJobsResponse>>(`${this.apiUrl}/job`);
  }

  public GetJob(jobId: number): Observable<ApiResult<JobModel>> {
    return this.httpClient.get<ApiResult<JobModel>>(`${this.apiUrl}/job/${jobId}`);
  }

  public CreateJob(job: JobModel): Promise<ApiResult<CreateJobResponse>> {
    return this.httpClient.post<ApiResult<CreateJobResponse>>(`${this.apiUrl}/job`, job).toPromise();
  }
}
