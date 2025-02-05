[Route("v1/logs")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly ILogStorage _logStorage;

    public LogsController(ILogStorage logStorage)
    {
        _logStorage = logStorage;
    }

    [HttpPost]
    public async Task<IActionResult> StoreLog([FromBody] LogEntry logEntry)
    {
        // Validation
        if (logEntry == null || string.IsNullOrEmpty(logEntry.Service) || string.IsNullOrEmpty(logEntry.Level) || string.IsNullOrEmpty(logEntry.Message))
        {
            return BadRequest("Invalid log entry.");
        }

        await _logStorage.StoreLogAsync(logEntry);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs([FromQuery] LogFilter filter)
    {
        var logs = await _logStorage.RetrieveLogsAsync(filter);
        return Ok(logs);
    }
}
