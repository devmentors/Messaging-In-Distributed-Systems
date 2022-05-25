using Microsoft.EntityFrameworkCore;
using SuperStore.Shared.Accessors;

namespace SuperStore.Shared.Deduplication;

internal sealed class DeduplicationMessageHandlerDecorator<TMessage> : IMessageHandler<TMessage> where TMessage : class, IMessage
{
    private readonly IMessageHandler<TMessage> _handler;
    private readonly IMessageIdAccessor _messageIdAccessor;
    private readonly DbContext _dbContext;

    public DeduplicationMessageHandlerDecorator(IMessageHandler<TMessage> handler, Func<DbContext> getContext, 
        IMessageIdAccessor messageIdAccessor)
    {
        _handler = handler;
        _messageIdAccessor = messageIdAccessor;
        _dbContext = getContext();
    }

    public async Task HandleAsync(TMessage message)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var messageId = _messageIdAccessor.GetMessageId();
            
            if (await _dbContext.Set<DeduplicationModel>().AnyAsync(x => x.MessageId == messageId))
            {
                return;
            }

            await _handler.HandleAsync(message);

            var deduplicationModel = new DeduplicationModel {MessageId = messageId, ProcessedAt = DateTime.UtcNow};
            await _dbContext.Set<DeduplicationModel>().AddAsync(deduplicationModel);

            await transaction.CommitAsync();
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}