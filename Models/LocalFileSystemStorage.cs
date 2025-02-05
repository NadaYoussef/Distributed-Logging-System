using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Distributed_logging_System.Model
{
    public class LocalFileSystemStorage : ILogStorage
    {
        private readonly string _logDirectory;

        public LocalFileSystemStorage(string logDirectory)
        {
            _logDirectory = logDirectory;
        }

        public async Task StoreLogAsync(LogEntry logEntry)
        {
            var filePath = Path.Combine(_logDirectory, $"{logEntry.Service}.log");
            var logContent = JsonConvert.SerializeObject(logEntry) + Environment.NewLine;
            await File.AppendAllTextAsync(filePath, logContent);
        }

        public async Task<IEnumerable<LogEntry>> GetLogsAsync(string service, string level, DateTime? startTime, DateTime? endTime)
        {
            var filePath = Path.Combine(_logDirectory, $"{service}.log");

            if (!File.Exists(filePath)) return new List<LogEntry>();

            var logLines = await File.ReadAllLinesAsync(filePath);
            var logs = logLines
                .Select(line => JsonConvert.DeserializeObject<LogEntry>(line))
                .Where(log => (string.IsNullOrEmpty(service) || log.Service == service) &&
                              (string.IsNullOrEmpty(level) || log.Level == level) &&
                              (!startTime.HasValue || log.Timestamp >= startTime) &&
                              (!endTime.HasValue || log.Timestamp <= endTime))
                .ToList();

            return logs;
        }
    }

}
