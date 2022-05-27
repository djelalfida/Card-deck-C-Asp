namespace Howest.MagicCards.DAL.Repositories;

public class ArtistRepository : IArtistRepository
{



    private readonly mtg_v1Context _db;
    
    public ArtistRepository(mtg_v1Context db)
    {
        _db = db;
    }
    
    public Artist? GetArtistbyId(long id)
    {
        Artist? artist = _db.Artists
                            .SingleOrDefault(a => a.Id == id);

        return artist;
    }
}
