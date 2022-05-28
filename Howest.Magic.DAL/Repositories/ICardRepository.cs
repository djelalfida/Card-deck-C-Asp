namespace Howest.MagicCards.DAL.Repositories
{
    public interface ICardRepository
    {
        IQueryable<Card> GetAllCards();
        IQueryable<Rarity> GetRarities();
    }
}
