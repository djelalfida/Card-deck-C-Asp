using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories;

public class DeckRepository : IDeckRepository
{
    private readonly mtg_v1Context _db;

    public DeckRepository(mtg_v1Context mtgContext)
    {
        _db = mtgContext;
    }
    
    public Deckscard? AddDeck(long cardId, string name)
    {
        var deck = new Deckscard
        {
            CardId = cardId,
            Name = name
        };

        _db.Deckscards.Add(deck);

        Save();
        
        return deck;
    }

    public Deckscard? GetDeck(string name)
    {
        return _db.Deckscards
            .Include(d => d.Card)
            .FirstOrDefault(d => d.Name == name);
    }

    public IQueryable<Deckscard> GetAllDecks()
    {
                IQueryable <Deckscard> allDecks = _db.Deckscards
                                        .Include(d => d.Card)
                                       .Select(b => b);

        return allDecks;
    }    

    public Deckscard? DeleteDeck(string name)
    {
        Deckscard? deck = GetDeck(name);

        if (deck is Deckscard)
        {
            _db.Deckscards.RemoveRange(_db.Deckscards.Where(d => d.Name == name));

            Save();
        }
        

        return deck;
    }

    private bool Save()
    {
        return _db.SaveChanges() > 0;
    }
}
