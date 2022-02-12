using SuperStore.Carts.Messages;
using SuperStore.Shared;

namespace SuperStore.Carts.Services;

internal sealed class MessagingBackgroundService : BackgroundService
{
    private readonly IMessageSubscriber _messageSubscriber;
    private readonly ILogger<MessagingBackgroundService> _logger;

    public MessagingBackgroundService(IMessageSubscriber messageSubscriber, ILogger<MessagingBackgroundService> logger)
    {
        _messageSubscriber = messageSubscriber;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageSubscriber
            .SubscribeMessage<FundsMessage>("carts-service-eu-many-words-queue", "EU.#", "Funds",
                (msg, args) =>
                {
                    _logger.LogInformation(
                        $"Received EU multiple-word message for customer: {msg.CustomerId} | Funds: {msg.CurrentFunds} | RoutingKey: {args.RoutingKey}");
                    return Task.CompletedTask;
                })
            .SubscribeMessage<FundsMessage>("carts-service-eu-single-word-queue", "EU.*", "Funds",
                (msg, args) =>
                {
                    _logger.LogInformation(
                        $"Received EU single-word message for customer: {msg.CustomerId} | Funds: {msg.CurrentFunds} | RoutingKey: {args.RoutingKey}");
                    return Task.CompletedTask;
                });
    }
}