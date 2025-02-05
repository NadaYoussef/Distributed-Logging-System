using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Distributed_logging_System.Model
{
    public class S3CompatibleStorage : ILogStorage
    {
        private readonly HttpClient _httpClient;
        private readonly string _s3Url;

        public S3CompatibleStorage(HttpClient httpClient, string s3Url)
        {
            _httpClient = httpClient;
            _s3Url = s3Url;
        }

        public async Task StoreLogAsync(LogEntry logEntry)
        {
            // Convert log entry to JSON and send to S3-compatible HTTP API.
            var content = new StringContent(JsonConvert.SerializeObject(logEntry), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_s3Url}/logs", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<LogEntry>> GetLogsAsync(string service, string level, DateTime? startTime, DateTime? endTime)
        {
            // Send GET request to S3 compatible service to retrieve logs.
            var response = await _httpClient.GetAsync($"{_s3Url}/logs?service={service}&level={level}&startTime={startTime}&endTime={endTime}");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<LogEntry>>(content);
        }
    }

}
