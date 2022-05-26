﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO;

public record CardReadDTO
{
    public string? Name { get; init; }
    public string? ManaCost { get; init; }
    public string? ConvertedManaCost { get; init; }
    public string? Type { get; init; }
    public string? RarityCode { get; init; }
    public string? SetCode { get; init; }
    public string? Text { get; init; }
    public string? Flavor { get; init; }
    public string? ArtistId { get; init; }
    public string? Number { get; init; }
    public string? Power { get; init; }
    public string? Toughness { get; init; }
    public string? Layout { get; init; }
    public string? MultiverseId { get; init; }
    public string? OriginalImageUrl { get; init; }
    public string? Image { get; init; }
    public string? OriginalText { get; init; }
    public string? OriginalType { get; init; }
    public string? MtgId { get; init; }
    public string? Variations { get; init; }

}
