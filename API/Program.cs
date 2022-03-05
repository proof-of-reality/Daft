using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Common;
using Core.Interfaces;
using Core.Models;
using Infrastructure.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Configure());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SqlServerContext>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void AddTestData(SqlServerContext context)
{
    var otavio = new Client("otoavbio", "nunes", "vojnedofin@gmail.com", "12876137987");
    var bruna = new Client("bruna", "albuqyerer", "bruna@gmail.com", "1333308233");

    var flat = new Property("a274", OfferPurpose.Rent, PropertyType.Flat, 370);
    otavio.Add(flat);

    var appartment = new Property("d15a274", OfferPurpose.Rent, PropertyType.Appartment, 590);
    bruna.Add(appartment);

    context.Clients.Add(otavio);
    context.Clients.Add(bruna);

    context.Properties.Add(flat);
    context.Properties.Add(appartment);

    context.SaveChanges();
}
