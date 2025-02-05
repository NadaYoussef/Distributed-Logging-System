import { Component, OnInit } from '@angular/core';
import { LogService } from '../../services/log.service';
import { LogEntry, LogFilter } from '../../models/log-entry.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-log-list',
  templateUrl: './log-list.component.html',
  styleUrls: ['./log-list.component.css'],
})
export class LogListComponent implements OnInit {
  logs: LogEntry[] = [];
  displayedColumns: string[] = ['timestamp', 'service', 'level', 'message'];
  dataSource: MatTableDataSource<LogEntry> = new MatTableDataSource();
  filters: LogFilter = {};
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  @ViewChild(MatSort) sort: MatSort | undefined;

  
  constructor(private logService: LogService) {}

  ngOnInit(): void {
    this.getLogs();  
  }
 
  getLogs(): void {
    this.logService.getLogs(this.filters).subscribe((logs) => {
      this.logs = logs;
      this.dataSource.data = this.logs;
    });
  }

  applyFilter(service: string, level: string, startTime: string, endTime: string): void {
    this.filters = {
      service,
      level,
      startTime: startTime,
      endTime: endTime,
    };
    this.getLogs();
  }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
