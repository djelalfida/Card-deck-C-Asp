using Howest.MagicCards.DAL.Models;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<mtg_v1Context>
    (options => options.UseSqlServer(config.GetConnectionString("mtg_v1")));


builder.Services.AddScoped<ICardRepository, CardRepository>();



builder.Services.AddAutoMapper(new System.Type[] {
                                             typeof(Howest.MagicCards.Shared.Mappings.CardsProfile)});



var app = builder.Build();

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
