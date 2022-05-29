using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<mtg_v1Context>
    (options => options.UseSqlServer(config.GetConnectionString("mtg_v1")));


builder.Services.AddScoped<ICardRepository, CardRepository>();



builder.Services.AddAutoMapper(new System.Type[] {
                                             typeof(Howest.MagicCards.Shared.Mappings.CardsProfile)});

builder.Services.AddHttpClient("CardsAPI", client =>
{
    client.BaseAddress = new Uri(config.GetValue<string>("ApiLink"));
});

builder.Services.AddHttpClient("DecksAPI", client =>
{
    client.BaseAddress = new Uri(config.GetValue<string>("MinimalApiDeckLink"));
});



WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
