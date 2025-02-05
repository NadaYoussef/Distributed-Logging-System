public class MqLogStorage : ILogStorage
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MqLogStorage(string MqConnectionString)
    {
        var factory = new ConnectionFactory() { Uri = new Uri(MqConnectionString) };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "logs", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public async Task StoreLogAsync(LogEntry logEntry)
    {
        var message = JsonSerializer.Serialize(logEntry);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "", routingKey: "logs", basicProperties: null, body: body);
    }

    public async Task<List<LogEntry>> RetrieveLogsAsync(LogFilter filter)
    {
        var logs = new List<LogEntry>();
        return logs;
    }
}
