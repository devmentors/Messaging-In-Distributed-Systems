using SuperStore.Funds.Messages;
using SuperStore.Shared;
using SuperStore.Shared.Publishers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMessaging();

var app = builder.Build();

app.MapGet("/", () => "Funds Service!");
app.MapGet("/message/send/EU/{country}", async (IMessagePublisher messagePublisher, string country) =>
{
    var message = new FundsMessage(123, 10.00m);
    await messagePublisher.PublishAsync("Funds", $"EU.{country}", message);
});

app.Run();
