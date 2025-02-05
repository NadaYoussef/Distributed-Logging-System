using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Distributed_logging_System.Model
{
    public class LogDbContext : DbContext
    {
        public DbSet<LogEntry> Logs { get; set; }
    }

    public class DatabaseStorage : ILogStorage
    {
        private readonly LogDbContext _dbContext;

        public DatabaseStorage(LogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task StoreLogAsync(LogEntry logEntry)
        {
            await _dbContext.Logs.AddAsync(logEntry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<LogEntry>> GetLogsAsync(string service, string level, DateTime? startTime, DateTime? endTime)
        {
            var query = _dbContext.Logs.AsQueryable();

            if (!string.IsNullOrEmpty(service))
                query = query.Where(log => log.Service == service);

            if (!string.IsNullOrEmpty(level))
                query = query.Where(log => log.Level == level);

            if (startTime.HasValue)
                query = query.Where(log => log.Timestamp >= startTime);

            if (endTime.HasValue)
                query = query.Where(log => log.Timestamp <= endTime);

            return await query.ToListAsync();
        }
    }

}
