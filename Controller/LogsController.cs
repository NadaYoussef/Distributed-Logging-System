using Distributed_logging_System.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Distributed-Logging-System.Controllers
{
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
        public async Task<IActionResult> PostLog([FromBody] LogEntry logEntry)
        {
            if (logEntry == null || !Enum.IsDefined(typeof(LogLevel), logEntry.Level))
                return BadRequest("Invalid log entry.");

            await _logStorage.StoreLogAsync(logEntry);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery] string service, [FromQuery] string level,
                                                  [FromQuery] DateTime? startTime, [FromQuery] DateTime? endTime)
        {
            var logs = await _logStorage.GetLogsAsync(service, level, startTime, endTime);
            return Ok(logs);
        }
    }
}
