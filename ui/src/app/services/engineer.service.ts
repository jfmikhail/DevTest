import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EngineerService {

  private apiUrl;
  constructor(private httpClient: HttpClient) {
    this.apiUrl = environment.apiUrl;
   }

  public GetEngineers(): Observable<string[]> {
    return this.httpClient.get<string[]>(`${this.apiUrl}/engineer`);
  }
}
