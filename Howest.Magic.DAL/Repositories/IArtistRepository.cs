namespace Howest.MagicCards.DAL.Repositories;

public interface IArtistRepository
{
    IEnumerable<Artist> GetAllArtists(int limit);
    IEnumerable<Card> GetRelatedCards(long id);
    Artist? GetArtistbyId(long id);
}
