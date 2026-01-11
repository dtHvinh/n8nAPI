using n8nAPI.APIWrapper.Common.Extensions;
using n8nAPI.Common.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAdditionConfigurations();

builder.Services.AddOpenApi();
builder.Services.RegisterServices();
builder.Services.RegisterRateLimiter();
builder.Services.AddN8nClient(cf =>
{
    cf.ApiKey = builder.Configuration["N8n:ApiKey"];
    cf.BaseUrl = builder.Configuration["N8n:BaseUrl"];
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/docs/api.json");
    app.MapScalarApiReference(c =>
    {
        c.OpenApiRoutePattern = "/docs/api.json";
    });
}

app.UseHttpsRedirection();
app.RegisterEndpoints(cf =>
{
    cf.Version = 1;
});
app.UseRateLimiter();

await app.RunAsync();
