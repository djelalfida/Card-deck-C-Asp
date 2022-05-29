using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtensions
{

    public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards, string setCode, int artistId, string rarityCode, string name, string originalText)
    {
        if (!string.IsNullOrEmpty(setCode))
        {
            cards = cards.Where(c => c.SetCode == setCode);
        }

        if (artistId > 0)
        {
            cards = cards.Where(c => c.ArtistId == artistId);
        }

        if (!string.IsNullOrEmpty(rarityCode))
        {
            cards = cards.Where(c => c.RarityCode == rarityCode);
        }

        if (!string.IsNullOrEmpty(name))
        {
            cards = cards.Where(c => c.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(originalText))
        {
            cards = cards.Where(c => c.OriginalText.Contains(originalText));
        }

        return cards;
    }

    public static IQueryable<Card> Sort(this IQueryable<Card> cards, string sort)
    {
        if (string.IsNullOrEmpty(sort))
        {
            return cards;
        }


        return sort.StartsWith("asc") || sort == "1" ? cards.OrderBy(c => c.Name) : cards.OrderByDescending(c => c.Name);

    }

}
