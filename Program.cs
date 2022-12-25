using Hangfire;
using Hangfire.Api.Services;
using Hangfire.Dashboard;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(configs=>
    configs.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage(builder.Configuration.GetConnectionString ("DefaultConnection")));
builder.Services.AddScoped<IServiceManagement, ServiceManagement>();
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    DashboardTitle = "Drivers Dashboard",
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter()
        {
            Pass="12345",
            User="ahmed"
        }
    }
});
app.MapHangfireDashboard();
//RecurringJob.AddOrUpdate<IServiceManagement>(s => s.SendEmail(),/*Cron.Minutely*/ "0 * * ? * *");//for periodic execution (every minute)

app.Run();
