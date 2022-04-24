using SuperStore.Carts.DAL;
using SuperStore.Carts.Services;
using SuperStore.Shared;
using SuperStore.Shared.Deduplication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMessaging(c => c.AddDeduplication<CartsDbContext>(builder.Configuration));
builder.Services.AddDataAccess();
builder.Services.AddHostedService<MessagingBackgroundService>();

var app = builder.Build();

app.MapGet("/", () => "Carts Service!");

var scope = app.Services.CreateScope();
var ctx = scope.ServiceProvider.GetRequiredService<CartsDbContext>();
ctx.Database.EnsureCreated();

app.Run();