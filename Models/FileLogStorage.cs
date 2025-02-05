public class FileLogStorage : ILogStorage
{
    private readonly string _logDirectory;

    public FileLogStorage(string logDirectory)
    {
        _logDirectory = logDirectory;
    }

    public async Task StoreLogAsync(LogEntry logEntry)
    {
        var filePath = Path.Combine(_logDirectory, $"{logEntry.Timestamp:yyyyMMdd}.log");

        var logMessage = JsonSerializer.Serialize(logEntry) + Environment.NewLine;
        await File.AppendAllTextAsync(filePath, logMessage);
    }

    public async Task<List<LogEntry>> RetrieveLogsAsync(LogFilter filter)
    {
        var logFiles = Directory.GetFiles(_logDirectory, "*.log");
        var logs = new List<LogEntry>();

        foreach (var file in logFiles)
        {
            var logLines = await File.ReadAllLinesAsync(file);
            foreach (var line in logLines)
            {
                var logEntry = JsonSerializer.Deserialize<LogEntry>(line);
                if (logEntry != null && (filter.Service == null || logEntry.Service == filter.Service) &&
                    (filter.Level == null || logEntry.Level == filter.Level) &&
                    (!filter.StartTime.HasValue || logEntry.Timestamp >= filter.StartTime) &&
                    (!filter.EndTime.HasValue || logEntry.Timestamp <= filter.EndTime))
                {
                    logs.Add(logEntry);
                }
            }
        }

        return logs;
    }
}
