namespace Howest.MagicCards.DAL.Repositories;

public interface IArtistRepository
{
    IEnumerable<Artist> GetAllArtists();
    Artist? GetArtistbyId(long id);
}
