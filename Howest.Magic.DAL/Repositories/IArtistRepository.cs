namespace Howest.MagicCards.DAL.Repositories;

public interface IArtistRepository
{
    IEnumerable<Artist> GetAllArtists();
    IEnumerable<Card> GetRelatedCards(long id);
    Artist? GetArtistbyId(long id);
}
