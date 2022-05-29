using Microsoft.EntityFrameworkCore;

const string defaultPrefix = "/api";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.
builder.Services.AddDecksServices(config);


WebApplication app = builder.Build();
string urlPrefix = config.GetSection("ApiPrefix").Value ?? defaultPrefix;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapDecksEndpoints(urlPrefix);

app.Run();
