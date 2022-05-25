using Microsoft.Extensions.DependencyInjection;

namespace SuperStore.Shared;

public interface IMessagingConfiguration
{
    IServiceCollection Services { get; }
}

internal sealed record MessagingConfiguration(IServiceCollection Services) : IMessagingConfiguration;