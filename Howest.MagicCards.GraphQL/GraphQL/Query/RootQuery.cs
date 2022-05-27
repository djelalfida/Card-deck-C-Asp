using GraphQL;

namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class RootQuery : ObjectGraphType
{
    public RootQuery(ICardRepository cardRepository)
    {
        Name = "Query";

        Field<ListGraphType<CardType>>(
            "cards",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "power" },
                new QueryArgument<StringGraphType> { Name = "toughness" }
            ),
            resolve: context =>
            {
                string power = context.GetArgument<string>("power");
                string toughness = context.GetArgument<string>("toughness");

                return cardRepository
                    .GetAllCards()
                    .Where(c =>
                        (power == null || c.Power == power) &&
                        (toughness == null || c.Toughness == toughness))
                    .ToList();
            }
        );
    }
}
