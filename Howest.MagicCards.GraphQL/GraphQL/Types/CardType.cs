namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class CardType: ObjectGraphType<Card>
{
    public CardType(ICardRepository cardRepository)
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


    }
}
