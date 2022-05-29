using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.MinimalAPI.Endpoints;

public static class DecksEndpoints
{
    public static void MapDecksEndpoints(this WebApplication app, string urlPrefix)
    {
        app.MapPost($"{urlPrefix}/decks/create", AddDeck)
            .Accepts<DeckWriteDTO>("application/json")
            .Produces<DeckWriteDTO>(StatusCodes.Status201Created)
            .WithTags("Decks");
    }
    
    public static void AddDecksServices(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddScoped<IDeckRepository, DeckRepository>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();




        services.AddDbContext<mtg_v1Context>
            (options => options.UseSqlServer(config.GetConnectionString("mtg_v1")));        



        services.AddScoped<IDeckRepository, DeckRepository>();



        services.AddAutoMapper(new System.Type[] {
                                             typeof(Howest.MagicCards.Shared.Mappings.ArtistsProfile),
                                             typeof(Howest.MagicCards.Shared.Mappings.DecksProfile),
                                             typeof(Howest.MagicCards.Shared.Mappings.CardsProfile)});
    }

    private static IResult AddDeck(IDeckRepository repo, DeckWriteDTO deck)
    {
        foreach (long card in deck.Cards)
        {
            repo.AddDeck(card, deck.Name);
        }

        return Results.Ok($"Deck {deck.Name} added");
    }
}
