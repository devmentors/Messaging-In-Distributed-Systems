namespace SuperStore.Shared;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(string exchange, string routingKey, TMessage message) where TMessage : class, IMessage;
}