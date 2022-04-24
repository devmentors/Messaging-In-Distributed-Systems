namespace SuperStore.Shared.Publishers;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(string exchange, string routingKey, TMessage message, string messageId = default) 
        where TMessage : class, IMessage;
}