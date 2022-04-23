using SuperStore.Carts.Messages;
using SuperStore.Shared;
using SuperStore.Shared.Dispatchers;
using SuperStore.Shared.Subscribers;

namespace SuperStore.Carts.Services;

public class MessagingBackgroundService : BackgroundService
{
    private readonly IMessageSubscriber _messageSubscriber;
    private readonly IMessageDispatcher _dispatcher;
    private readonly ILogger<MessagingBackgroundService> _logger;

    public MessagingBackgroundService(IMessageSubscriber messageSubscriber, IMessageDispatcher dispatcher,
        ILogger<MessagingBackgroundService> logger)
    {
        _messageSubscriber = messageSubscriber;
        _dispatcher = dispatcher;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageSubscriber
            .SubscribeMessage<FundsMessage>("carts-service-eu-many-words-queue", "EU.#", "Funds",
                async (msg, args) =>
                {
                    _logger.LogInformation(
                        $"Received EU multiple-word message for customer: {msg.CustomerId} | Funds: {msg.CurrentFunds} | RoutingKey: {args.RoutingKey}");
                    await _dispatcher.DispatchAsync(msg);
                })
            .SubscribeMessage<FundsMessage>("carts-service-eu-single-word-queue", "EU.*", "Funds",
                async (msg, args) =>
                {
                    _logger.LogInformation(
                        $"Received EU single-word message for customer: {msg.CustomerId} | Funds: {msg.CurrentFunds} | RoutingKey: {args.RoutingKey}");
                    await _dispatcher.DispatchAsync(msg);
                });
    }
}