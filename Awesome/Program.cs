var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = "InstrumentationKey=fe73c298-de3d-481e-aba7-6c6c5e2d5f8b;IngestionEndpoint=https://germanywestcentral-1.in.applicationinsights.azure.com/";
});

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(builder =>
{
    builder.MapControllers();
});

await app.RunAsync();
