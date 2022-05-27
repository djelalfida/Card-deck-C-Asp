namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class CardType: ObjectGraphType<Card>
{
    public CardType(ICardRepository cardRepository, IArtistRepository artistRepository)
    {

    Name = "Card";

        Field(c => c.Id, type: typeof(IdGraphType))
            .Description("The id of the card")
            .Name("Id");

        Field(c => c.Name, type: typeof(StringGraphType))
            .Description("The name of the card")
            .Name("Name");

        Field(c => c.ManaCost, type: typeof(StringGraphType))
            .Description("The mana cost of the card")
            .Name("ManaCost");

        Field(c => c.ConvertedManaCost, type: typeof(StringGraphType))
            .Description("The converted mana cost of the card")
            .Name("ConvertedManaCost");

        Field(c => c.Type, type: typeof(StringGraphType))
            .Description("The type of the card")
            .Name("Type");

        
        
        Field(c => c.RarityCode, type: typeof(StringGraphType))
            .Description("The rarity code of the card")
            .Name("RarityCode");

        Field(c => c.SetCode, type: typeof(StringGraphType))
            .Description("The set code of the card")
            .Name("SetCode");

        Field(c => c.Text, type: typeof(StringGraphType))
            .Description("The text of the card")
            .Name("Text");

        Field(c => c.Flavor, type: typeof(StringGraphType))
            .Description("The flavor text of the card")
            .Name("Flavor");

        Field(c => c.ArtistId, type: typeof(StringGraphType))
            .Description("The artist id of the card")
            .Name("ArtistId");

        Field(c => c.Number, type: typeof(StringGraphType))
            .Description("The number of the card")
            .Name("Number");

        Field(c => c.Power, type: typeof(StringGraphType))
            .Description("The power of the card")
            .Name("Power");

        Field(c => c.Toughness, type: typeof(StringGraphType))
            .Description("The toughness of the card")
            .Name("Toughness");

        Field(c => c.Layout, type: typeof(StringGraphType))
            .Description("The layout of the card")
            .Name("Layout");

        Field(c => c.MultiverseId, type: typeof(StringGraphType))
            .Description("The multiverse id of the card")
            .Name("MultiverseId");

        Field(c => c.OriginalImageUrl, type: typeof(StringGraphType))
            .Description("The original image url of the card")
            .Name("OriginalImageUrl");

        Field(c => c.Image, type: typeof(StringGraphType))
            .Description("The image of the card")
            .Name("Image");

        Field(c => c.OriginalText, type: typeof(StringGraphType))
            .Description("The original text of the card")
            .Name("OriginalText");

        Field(c => c.OriginalType, type: typeof(StringGraphType))
            .Description("The original type of the card")
            .Name("OriginalType");

        Field(c => c.MtgId, type: typeof(StringGraphType))
            .Description("The mtg id of the card")
            .Name("MtgId");

        Field(c => c.Variations, type: typeof(StringGraphType))
            .Description("The variations of the card")
            .Name("Variations");

        
        Field<ArtistType>(
            "artist",
            resolve: context => artistRepository.GetArtistbyId(context.Source.ArtistId ?? default)
        );






    }
}
