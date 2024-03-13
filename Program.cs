using GameStore.Api.Authorization;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Middleware;
using GameStore.Api.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameStoreAuthorization();
builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

/*builder.Logging.AddJsonConsole(options => {
    options.JsonWriterOptions = new(){
        Indented = true
    };
});
*/

builder.Services.AddHttpLogging(options => { });
var app = builder.Build();
app.UseHttpLogging();
app.UseMiddleware<RequestTimerMiddleware>();
app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());
await app.Services.InitializeDbAsync();

app.MapGamesEndpoints();
app.Run();
