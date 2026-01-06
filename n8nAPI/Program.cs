using n8nAPI.Common.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.RegisterServices();

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

await app.RunAsync();
