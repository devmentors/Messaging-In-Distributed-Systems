using SuperStore.Shared;

namespace SuperStore.Funds.Messages;

public record FundsMessage(long CustomerId, decimal CurrentFunds) :  IMessage;