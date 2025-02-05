export interface LogEntry {
  service: string;
  level: string;
  message: string;
  timeStamp: string;
}

export interface LogFilter {
  service?: string;
  level?: string;
  startTime?: string;   
  endTime?: string;    
}
