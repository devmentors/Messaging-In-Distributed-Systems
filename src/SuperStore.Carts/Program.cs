using SuperStore.Carts.Services;
using SuperStore.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMessaging();
builder.Services.AddHostedService<MessagingBackgroundService>();

var app = builder.Build();

app.MapGet("/", () => "Carts Service!");

app.Run();