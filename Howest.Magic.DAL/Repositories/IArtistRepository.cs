namespace Howest.MagicCards.DAL.Repositories;

public interface IArtistRepository
{
    IQueryable<Artist> GetAllArtists(int limit);
    IQueryable<Card> GetRelatedCards(long id);
    Artist? GetArtistbyId(long id);
}
