using RabbitMQ.Client;

namespace SuperStore.Shared.Connections;

public interface IChannelFactory
{
    IModel Create();
}