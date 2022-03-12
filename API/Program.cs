using System.Reflection;
using Core.Interfaces;
using Core.Models;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Infrastructure.SqlServer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        opt.SerializerSettings.ContractResolver = new UserConverter();
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SqlServerContext>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("Hangfire"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));
builder.Services.AddHangfireServer();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseHangfireDashboard("/hangfire", new DashboardOptions { Authorization = new[] { new AllowAny() } });
app.UseAuthorization();
app.MapControllers();


app.Run();

class UserConverter : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);

        if (property.DeclaringType == typeof(User) && property.PropertyName == "Password")
        {
            property.ShouldSerialize = _ => false;
        }
        return property;
    }
}

public class AllowAny : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        // Allow all authenticated users to see the Dashboard (potentially dangerous).
        return true;
    }
}