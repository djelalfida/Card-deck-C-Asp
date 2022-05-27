namespace Howest.MagicCards.DAL.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly mtg_v1Context _db;
        
        public CardRepository(mtg_v1Context mtgContext)
        {
            _db = mtgContext;
        }

        
        public IQueryable<Card> GetAllCards()
        {
            IQueryable<Card> allCards = _db.Cards
                                           .Select(b => b);

            return allCards;
        }
    }
}
