using Microsoft.EntityFrameworkCore;
using SuperStore.Carts.DAL;
using SuperStore.Carts.DAL.Models;
using SuperStore.Shared;

namespace SuperStore.Carts.Messages.Handlers;

internal sealed class FundsMessageHandler : IMessageHandler<FundsMessage>
{
    private readonly CartsDbContext _dbContext;

    public FundsMessageHandler(CartsDbContext dbContext)
        => _dbContext = dbContext;

    public async Task HandleAsync(FundsMessage message)
    {
        var funds = await _dbContext.CustomerFunds.SingleOrDefaultAsync(x => x.CustomerId == message.CustomerId);

        if (funds is null)
        {
            funds = new CustomerFundsModel
            {
                CustomerId = message.CustomerId,
                CurrentFunds = message.CurrentFunds
            };

            await _dbContext.CustomerFunds.AddAsync(funds);
            return;
        }

        funds.CurrentFunds = message.CurrentFunds;
        _dbContext.CustomerFunds.Update(funds);
    }
}