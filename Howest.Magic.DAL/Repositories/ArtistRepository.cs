namespace Howest.MagicCards.DAL.Repositories;

public class ArtistRepository : IArtistRepository
{



    private readonly mtg_v1Context _db;
    
    public ArtistRepository(mtg_v1Context db)
    {
        _db = db;
    }

    public IQueryable<Artist> GetAllArtists(int limit)
    {
        IQueryable<Artist> allArtists = _db.Artists
                                            .Select(a => a);

        return limit > 0 ? allArtists.Take(limit) : allArtists;
    }

    public IQueryable<Card> GetRelatedCards(long id) {
        IQueryable<Card> relatedCards = _db.Cards
                                            .Where(c => c.ArtistId == id)
                                            .Select(c => c);

        return relatedCards;
    }

    public Artist? GetArtistbyId(long id)
    {
        Artist? artist = _db.Artists
                            .SingleOrDefault(a => a.Id == id);

        return artist;
    }
}
