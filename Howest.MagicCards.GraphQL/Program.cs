using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddDbContext<mtg_v1Context>
    (options => options.UseSqlServer(config.GetConnectionString("mtg_v1")));
builder.Services.AddScoped<ICardRepository, CardRepository>();

builder.Services.AddScoped<RootSchema>();
builder.Services.AddGraphQL()
                .AddGraphTypes(typeof(RootSchema), ServiceLifetime.Scoped)
                .AddDataLoader()
                .AddSystemTextJson();

var app = builder.Build();

app.UseGraphQL<RootSchema>();
app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions()
{
    EditorTheme = EditorTheme.Light
}
);



app.Run();

