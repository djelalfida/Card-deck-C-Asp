namespace Howest.MagicCards.Shared.Mappings;

public class ArtistsProfile: Profile
{
    public ArtistsProfile()
    {
        CreateMap<Artist, ArtistReadDTO>();
    }
}
