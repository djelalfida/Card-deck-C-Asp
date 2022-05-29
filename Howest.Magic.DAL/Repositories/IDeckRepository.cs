using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories;

public interface IDeckRepository
{
    Deckscard? AddDeck(long cardId, string name);
    Deckscard? GetDeck(string name);
    Deckscard? DeleteDeck(string name);
    IQueryable<Deckscard> GetAllDecks();
}
