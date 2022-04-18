namespace SuperStore.Shared;

public interface IMessageDispatcher
{
    Task DispatchAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
}