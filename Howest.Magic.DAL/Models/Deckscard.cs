using System;
using System.Collections.Generic;

namespace Howest.MagicCards.DAL.Models
{
    public partial class Deckscard
    {
        public long CardId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Card Card { get; set; } = null!;
    }
}
