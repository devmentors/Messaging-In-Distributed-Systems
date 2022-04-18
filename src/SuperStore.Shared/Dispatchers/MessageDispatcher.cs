using Microsoft.Extensions.DependencyInjection;

namespace SuperStore.Shared.Dispatchers;

internal sealed class MessageDispatcher : IMessageDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public MessageDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task DispatchAsync<TMessage>(TMessage message) where TMessage : class, IMessage
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<TMessage>>();
        await handler.HandleAsync(message);
    }
}