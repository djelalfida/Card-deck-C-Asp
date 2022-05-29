using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Mappings;

public class DecksProfile: Profile
{
    public DecksProfile()
    {
        CreateMap<DeckWriteDTO, Deckscard>();
        CreateMap<Deckscard, DeckReadDTO>()
            .ForMember(dto => dto.Card, m => m.MapFrom(s => s.Card));


    }
}
