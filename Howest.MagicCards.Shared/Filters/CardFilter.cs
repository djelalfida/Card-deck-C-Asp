using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Filters;

public class CardFilter : PaginationFilter
{
    
    public string SetCode { get; init; } = string.Empty;
    public int ArtistId { get; init; } = 0;
    public string RarityCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string OriginalText { get; init; } = string.Empty;
    public string? Sort { get; init; }

    // create toString
    public override string ToString()
    {
        return $"PageSize: {base.PageSize}, PageNumber: {base.PageNumber}, MaxPageSize: {base.MaxPageSize}, SetCode: {SetCode}, ArtistId: {ArtistId}, RarityCode: {RarityCode}, Name: {Name}, OriginalText: {OriginalText}, Sort: {Sort}";
    }

}
