using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO;

public record DeckReadDTO
{
    public string? Name { get; init; }
    public IEnumerable<Card>? Cards { get; init; }
}
