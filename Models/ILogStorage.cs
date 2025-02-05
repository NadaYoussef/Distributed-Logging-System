public interface ILogStorage
{
    Task StoreLogAsync(LogEntry logEntry);
    Task<List<LogEntry>> RetrieveLogsAsync(string service, string level, DateTime? startTime, DateTime? endTime);
}
