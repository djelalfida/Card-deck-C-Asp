using System;
using System.Collections.Generic;

namespace Howest.MagicCards.DAL.Models
{
    public partial class Deck
    {
        public Deck()
        {
            Cards = new HashSet<Card>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Deckscard> Deckscard { get; set; }        
    }
}
