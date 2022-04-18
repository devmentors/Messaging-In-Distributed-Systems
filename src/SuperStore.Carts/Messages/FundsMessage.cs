using SuperStore.Shared;

namespace SuperStore.Carts.Messages;

record FundsMessage(long CustomerId, decimal CurrentFunds) : IMessage;