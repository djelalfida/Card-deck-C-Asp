namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class RootQuery : ObjectGraphType
{
    public RootQuery(ICardRepository cardRepository)
    {
        Name = "Query";

        Field<ListGraphType<CardType>>(
            "cards",
            resolve: context => cardRepository
                                .GetAllCards()
                                .ToList()
        );
    }
}
