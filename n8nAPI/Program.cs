using n8nAPI.Common.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.RegisterServices();
builder.Services.RegisterRateLimiter();

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
