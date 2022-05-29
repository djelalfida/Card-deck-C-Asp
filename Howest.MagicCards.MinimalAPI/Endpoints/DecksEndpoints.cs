using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.MinimalAPI.Endpoints;

public static class DecksEndpoints
{
    public static void MapDecksEndpoints(this WebApplication app, string urlPrefix)
    {
        app.MapGet($"{urlPrefix}/test", () => "Hello World!");
    }

    public static void AddDecksServices(this IServiceCollection services)
    {
        
    }
}
