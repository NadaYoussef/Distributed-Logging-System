import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LogEntry {
  service: string;
  level: string;
  message: string;
  timestamp: string;
}

@Injectable({
  providedIn: 'root'
})
export class LogService {
  private apiUrl = 'http://localhost:5000/v1/logs'; // Adjust URL based on your backend API

  constructor(private http: HttpClient) {}

  // Fetch logs with filters and pagination
  getLogs(
    service?: string,
    level?: string,
    startTime?: string,
    endTime?: string,
    page: number = 1,
    pageSize: number = 10
  ): Observable<LogEntry[]> {
    let params = new HttpParams();

    if (service) params = params.set('service', service);
    if (level) params = params.set('level', level);
    if (startTime) params = params.set('start_time', startTime);
    if (endTime) params = params.set('end_time', endTime);
    params = params.set('page', page.toString());
    params = params.set('pageSize', pageSize.toString());

    return this.http.get<LogEntry[]>(this.apiUrl, { params });
  }
  
  getLogDetails(id: string): Observable<LogEntry> {
    return this.http.get<LogEntry>(`${this.apiUrl}/${id}`);
  }
}
