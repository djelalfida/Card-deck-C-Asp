using Howest.MagicCards.DAL.Models; // fix later
using Howest.MagicCards.DAL.Repositories; // fix later
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Extensions;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private readonly ICardRepository _cardRepo;
        private readonly IMapper _mapper;

        public CardsController(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepo = cardRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<PagedResponse<IEnumerable<CardReadDTO>>> GetAllCards([FromQuery] PaginationFilter filter, [FromServices] IConfiguration config)
        {

            filter.MaxPageSize = int.Parse(config["maxPageSize"]);
            
            return (_cardRepo.GetAllCards() is IQueryable<Card> allCards)
                   ? Ok(new PagedResponse<IEnumerable<CardReadDTO>>(
                           allCards
                               .ToPagedList(filter.PageNumber, filter.PageSize)
                               .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                               .ToList(),
                           filter.PageNumber,
                           filter.PageSize)
                   {
                       TotalRecords = allCards.Count()
                   })
                   : NotFound(new Response<CardReadDTO>()
                   {
                       Succeeded = false,
                       Errors = new string[] { "404" },
                       Message = "No cards found "
                   }
                   );

        }



    }
}
