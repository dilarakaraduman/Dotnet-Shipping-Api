using Application.Carriers.Handler;
using Application.Concrete;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Infrastructure;
using Infrastructure.Repository.Abstract;
using Infrastructure.Repository.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ShippingDbContext>(options =>
    options.UseSqlServer(@"Server=DILARA;Database=DotnetShippingApi;Trusted_Connection=True;Encrypt=False"));

// Hangfire için SQL Server konfigürasyonu
builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("ShippingDbConnection")));

builder.Services.AddHangfireServer();

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<CarrierService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CarrierConfigurationService>();
builder.Services.AddScoped<CarrierReportService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateCarrierCommandHandler).Assembly));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Hangfire Dashboard'u ekleyelim
var storage = new SqlServerStorage(builder.Configuration.GetConnectionString("ShippingDbConnection"));
var server = new BackgroundJobServer(new BackgroundJobServerOptions
{
    Queues = new[] { "default" },
    WorkerCount = Environment.ProcessorCount * 5
}, storage);
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new CustomDashboardAuthorizationFilter() }
});
// Recurring Job'ı Tanımlayalım (Her saat başı çalışacak)
RecurringJob.AddOrUpdate<CarrierReportService>(
    "generate-carrier-reports",
    service => service.GenerateCarrierReports(),
    Cron.Hourly);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
