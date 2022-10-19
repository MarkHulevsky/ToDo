using System.Text;
using Common.Models;
using Mail.BusinessLogic.Models;
using Mail.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mail.BusinessLogic.BackgroundWorkers;

public class MessageQueueBackgroundService : BackgroundService
{
    private const string EXCHANGE_NAME = "document_exchange";
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public MessageQueueBackgroundService(IOptions<RabbitMqSettingsModel> rabbitMqSettings, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var connectionFactory = new ConnectionFactory
        {
            HostName = rabbitMqSettings.Value.Host,
            Port = rabbitMqSettings.Value.Port,
            UserName = rabbitMqSettings.Value.UserName,
            Password = rabbitMqSettings.Value.Password
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _queueName = _channel.QueueDeclare().QueueName;

        _channel.QueueBind(
            queue: _queueName,
            exchange: EXCHANGE_NAME,
            routingKey: string.Empty
        );
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, args) =>
        {
            string content = Encoding.UTF8.GetString(args.Body.ToArray());

            var message = JsonConvert.DeserializeObject<SendViaEmailQueueMessageModel>(content);

            using IServiceScope scope = _serviceProvider.CreateScope();

            var mailService = scope.ServiceProvider.GetRequiredService<IMailService>();

            await mailService.SendFileViaEmailAsync(message!);
        };

        _channel.BasicConsume(
            queue: _queueName,
            autoAck: true,
            consumer: consumer
        );

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();

        base.Dispose();
    }
}