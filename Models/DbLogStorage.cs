public class DbLogStorage : ILogStorage
{
    private readonly DbContext _dbContext;

    public DbLogStorage(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task StoreLogAsync(LogEntry logEntry)
    {
        _dbContext.LogEntries.Add(logEntry);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<LogEntry>> RetrieveLogsAsync(LogFilter filter)
    {
        IQueryable<LogEntry> query = _dbContext.LogEntries;

        if (!string.IsNullOrEmpty(filter.Service))
            query = query.Where(log => log.Service == filter.Service);

        if (!string.IsNullOrEmpty(filter.Level))
            query = query.Where(log => log.Level == filter.Level);

        if (filter.StartTime.HasValue)
            query = query.Where(log => log.Timestamp >= filter.StartTime.Value);

        if (filter.EndTime.HasValue)
            query = query.Where(log => log.Timestamp <= filter.EndTime.Value);

        return await query.ToListAsync();
    }
}
