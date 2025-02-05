import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LogService, LogEntry } from '../log.service';

@Component({
  selector: 'app-log-details',
  templateUrl: './log-details.component.html',
  styleUrls: ['./log-details.component.css']
})
export class LogDetailsComponent implements OnInit {
  log: LogEntry | undefined;

  constructor(
    private logService: LogService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const logId = this.route.snapshot.paramMap.get('id')!;
    this.logService.getLogDetails(logId).subscribe((log) => {
      this.log = log;
    });
  }
}
