import { Component, OnInit } from '@angular/core';
import { LogService, LogEntry } from '../log.service';

@Component({
  selector: 'app-log-list',
  templateUrl: './log-list.component.html',
  styleUrls: ['./log-list.component.css']
})
export class LogListComponent implements OnInit {
  logs: LogEntry[] = [];
  filter = {
    service: '',
    level: '',
    startTime: '',
    endTime: ''
  };
  currentPage = 1;
  pageSize = 10;
  totalLogs = 0; // This should be set by your backend response if pagination is supported

  constructor(private logService: LogService) {}

  ngOnInit(): void {
    this.loadLogs();
  }

  loadLogs(): void {
    const { service, level, startTime, endTime } = this.filter;
    this.logService
      .getLogs(service, level, startTime, endTime, this.currentPage, this.pageSize)
      .subscribe((logs) => {
        this.logs = logs; 
        this.totalLogs = 10000; 
      });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadLogs();
  }

  onFilterChange(): void {
    this.currentPage = 1; 
    this.loadLogs();
  }

  onClearFilter(): void {
    this.filter = {
      service: '',
      level: '',
      startTime: '',
      endTime: ''
    };
    this.loadLogs();
  }

  onRowClick(row: LogEntry): void {
    this.router.navigate(['/log', row.timestamp]);
  }
}
