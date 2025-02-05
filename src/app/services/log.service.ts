import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LogEntry, LogFilter } from '../models/log-entry.model';

@Injectable({
  providedIn: 'root',
})
export class LogService {
  private apiUrl = 'http://localhost:5000/v1/logs';  

  constructor(private http: HttpClient) {}

  getLogs(filters: LogFilter): Observable<LogEntry[]> {
    let params = new HttpParams();

    if (filters.service) params = params.set('service', filters.service);
    if (filters.level) params = params.set('level', filters.level);
    if (filters.startTime) params = params.set('startTime', filters.startTime);
    if (filters.endTime) params = params.set('endTime', filters.endTime);

    return this.http.get<LogEntry[]>(this.apiUrl, { params });
  }
}
