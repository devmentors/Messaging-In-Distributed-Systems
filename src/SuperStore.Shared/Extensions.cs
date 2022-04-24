using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SuperStore.Shared.Accessors;
using SuperStore.Shared.Connections;
using SuperStore.Shared.Dispatchers;
using SuperStore.Shared.Publishers;
using SuperStore.Shared.Subscribers;

namespace SuperStore.Shared;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, Action<IMessagingConfiguration> configure = default)
    {
        var factory = new ConnectionFactory {HostName = "localhost"};
        var connection = factory.CreateConnection();

        services.AddSingleton(connection);
        services.AddSingleton<ChannelAccessor>();
        services.AddSingleton<IChannelFactory, ChannelFactory>();
        services.AddSingleton<IMessagePublisher, MessagePublisher>();
        services.AddSingleton<IMessageSubscriber, MessageSubscriber>();
        services.AddSingleton<IMessageDispatcher, MessageDispatcher>();
        services.AddSingleton<IMessageIdAccessor, MessageIdAccessor>();

        services.Scan(cfg => cfg.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IMessageHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        configure?.Invoke(new MessagingConfiguration(services));
        
        return services;
    }
}