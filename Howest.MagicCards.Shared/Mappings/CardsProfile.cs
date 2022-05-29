namespace Howest.MagicCards.Shared.Mappings;

public class CardsProfile: Profile
{
    public CardsProfile()
    {
        CreateMap<Card, CardReadDTO>();

        CreateMap<Rarity, RarityReadDTO>();
    }
}
