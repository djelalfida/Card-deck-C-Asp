using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Filters;

public class CardFilter : PaginationFilter
{

    public string SetCode { get; init; } = String.Empty;
    public int ArtistId { get; init; } = 0;
    public string RarityCode { get; init; } = String.Empty;
    public string Name { get; init; } = String.Empty;
    public string OriginalText { get; init; } = String.Empty;

    // create toString
    public override string ToString()
    {
        return $"PageSize: {base.PageSize}, PageNumber: {base.PageNumber}, MaxPageSize: {base.MaxPageSize}, SetCode: {SetCode}, ArtistId: {ArtistId}, RarityCode: {RarityCode}, Name: {Name}, OriginalText: {OriginalText}";
    }

}
