using Common.Models;
using Document.BusinessLogic.MessageQueueClients.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Document.BusinessLogic.MessageQueueClients;

public class RabbitMqClient: IMessageQueueClient, IDisposable
{
    private const string EXCHANGE_NAME = "document_exchange";
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqClient(IOptions<RabbitMqSettingsModel> rabbitMqSettings)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = rabbitMqSettings.Value.Host,
            Port = rabbitMqSettings.Value.Port,
            UserName = rabbitMqSettings.Value.UserName,
            Password = rabbitMqSettings.Value.Password
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);
    }

    public void Enqueue(byte[] message)
    {
        if (_channel.IsClosed)
        {
            return;
        }

        _channel.BasicPublish(
            exchange: EXCHANGE_NAME,
            routingKey: string.Empty,
            basicProperties: null,
            body: message
        );
    }

    public void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
    }
}