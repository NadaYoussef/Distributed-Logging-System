public class S3LogStorage : ILogStorage
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint;

    public S3LogStorage(HttpClient httpClient, string endpoint)
    {
        _httpClient = httpClient;
        _endpoint = endpoint;
    }

    public async Task StoreLogAsync(LogEntry logEntry)
    {
        var jsonLog = JsonSerializer.Serialize(logEntry);
        var content = new StringContent(jsonLog, Encoding.UTF8, "application/json");

        // Send HTTP request to S3-compatible storage
        var response = await _httpClient.PostAsync(_endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to store log in S3.");
        }
    }

    public async Task<List<LogEntry>> RetrieveLogsAsync(LogFilter filter)
    {
        var url = $"{_endpoint}?service={filter.Service}&level={filter.Level}&start_time={filter.StartTime}&end_time={filter.EndTime}";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<LogEntry>>(response);
    }
}
