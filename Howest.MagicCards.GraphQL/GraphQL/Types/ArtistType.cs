namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class ArtistType: ObjectGraphType<Artist>
{
    public ArtistType(IArtistRepository artistRepository)
    {

        Field(a => a.Id, type: typeof(LongGraphType))
            .Description("The ID of the artist.")
            .Name("Id");

        Field(a => a.FullName, type: typeof(StringGraphType))
            .Description("The full name of the artist.")
            .Name("FullName");

        Field<ListGraphType<CardType>>(
            "cards",
            resolve: context => artistRepository.GetRelatedCards(context.Source.Id)
        );
    }
}
