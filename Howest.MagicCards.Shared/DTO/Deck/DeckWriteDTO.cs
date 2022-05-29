namespace Howest.MagicCards.Shared.DTO;

public record DeckWriteDTO
{
    public IEnumerable<long>? Cards { get; init; }
    public string? Name { get; init; }

}
