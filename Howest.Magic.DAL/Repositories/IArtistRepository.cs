namespace Howest.MagicCards.DAL.Repositories;

public interface IArtistRepository
{
    Artist? GetArtistbyId(long id);
}
