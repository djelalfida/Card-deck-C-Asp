using Howest.MagicCards.WebAPI.Wrappers;
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

        app.MapDelete($"{urlPrefix}/decks/{{name}}", DeleteDeck)
            .WithTags("Decks");

        app.MapGet($"{urlPrefix}/decks", GetDecks);
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
        if(repo.GetDeck(deck.Name) is not null)
        {
            return Results.Conflict(new Response<DeckWriteDTO>()
            {
                Succeeded = false,
                Errors = new string[] { "409" },
                Message = $"Deck with name {deck.Name} already exists"
            });
        }

        foreach (long card in deck.Cards)
        {
            repo.AddDeck(card, deck.Name);
        }
        
        return Results.Ok(new Response<DeckWriteDTO>()
        {
            Succeeded = true,
            Errors = new string[] { "" },
            Message = $"Deck {deck.Name} was successfully created!"
        });
    }

    private static IResult DeleteDeck(IDeckRepository repo, string name)
    {
        if (repo.GetDeck(name) is null)
        {
            return Results.NotFound(new Response<DeckWriteDTO>()
            {
                Succeeded = false,
                Errors = new string[] { "404" },
                Message = $"Deck with name {name} does not exist"
            });
        }
        
        repo.DeleteDeck(name);
        return Results.Ok(new Response<DeckWriteDTO>()
        {
            Succeeded = true,
            Errors = new string[] { "" },
            Message = $"Deck {name} was successfully deleted!"
        });
    }
    
    private static IResult GetDecks(IDeckRepository repo, IMapper mapper)
    {
        
        return (repo.GetAllDecks() is IQueryable<Deckscard> foundDeck)
                    ? Results.Ok(new Response<IEnumerable<DeckReadDTO>>(
                            foundDeck
                                   .ProjectTo<DeckReadDTO>(mapper.ConfigurationProvider)
                                    .GroupBy(x => x.Name)
                                    .Select(x => new DeckReadDTO()
                                    {
                                        Name = x.Key,
                                        Card = null,
                                        Cards = x.Select(y => y.Card).ToList()
                                    })
                                    .ToList()
                        ))
                    : Results.NotFound(new Response<DeckReadDTO>()
                    {
                        Succeeded = false,
                        Errors = new string[] { "404" },
                        Message = "No decks found"
                    });


    }
}


