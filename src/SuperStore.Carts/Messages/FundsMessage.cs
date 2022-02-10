using SuperStore.Shared;

namespace SuperStore.Carts.Messages;

public record FundsMessage(long CustomerId, decimal CurrentFunds) :  IMessage;