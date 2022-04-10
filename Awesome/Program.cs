using Awesome;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = "InstrumentationKey=fe73c298-de3d-481e-aba7-6c6c5e2d5f8b;IngestionEndpoint=https://germanywestcentral-1.in.applicationinsights.azure.com/";
});

builder.Services.AddDbContext<AwesomeDbContext>(options =>
{
    options.UseSqlServer("Server=awesome.database.windows.net;Initial Catalog=AwesomeSqlDatabase;User ID=roman;Password=0123Roma;Encrypt=True;TrustServerCertificate=False;");
});

builder.Services.AddHostedService<SqlHostedServer>();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(builder =>
{
    builder.MapControllers();
});

await app.RunAsync();
