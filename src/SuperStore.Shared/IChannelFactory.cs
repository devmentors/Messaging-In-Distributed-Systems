using RabbitMQ.Client;

namespace SuperStore.Shared;

public interface IChannelFactory
{
    IModel Create();
}