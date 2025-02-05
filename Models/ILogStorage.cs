public interface ILogStorage
{
    Task StoreLogAsync(LogEntry logEntry);
    Task<List<LogEntry>> RetrieveLogsAsync(LogFilter filter);
}
