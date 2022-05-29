const string defaultPrefix = "/api";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDecksServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();
ConfigurationManager config = builder.Configuration;
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
