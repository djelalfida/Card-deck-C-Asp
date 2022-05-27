using Howest.MagicCards.DAL.Models; // fix later
using Howest.MagicCards.DAL.Repositories; // fix later
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Extensions;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private readonly ICardRepository _cardRepo;
        private readonly IMapper _mapper;

        private readonly IMemoryCache _cache;

        public CardsController(ICardRepository cardRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _cardRepo = cardRepository;
            _mapper = mapper;
            _cache = memoryCache;
        }


        [HttpGet]
        public ActionResult<PagedResponse<IEnumerable<CardReadDTO>>> GetAllCards([FromQuery] CardFilter filter, [FromServices] IConfiguration config)
        {

            filter.MaxPageSize = int.Parse(config["maxPageSize"]);

            if (!_cache.TryGetValue($"allCards{filter}", out PagedResponse<IEnumerable<CardReadDTO>> cachedResult))
            {
                // all cards
                IQueryable<Card> allCards = _cardRepo.GetAllCards();
                cachedResult = new PagedResponse<IEnumerable<CardReadDTO>>(
                           allCards
                               .ToFilteredList(filter.SetCode, filter.ArtistId, filter.RarityCode, filter.Name, filter.OriginalText)
                               .ToPagedList(filter.PageNumber, filter.PageSize)
                               .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                               .ToList(),
                           filter.PageNumber,
                           filter.PageSize)
                {
                    TotalRecords = allCards.Count()
                };

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                };

                _cache.Set($"allCards{filter}", cachedResult, cacheOptions);
            }

            return (cachedResult is PagedResponse<IEnumerable<CardReadDTO>> cards) ? Ok(cards) : NotFound(new Response<CardReadDTO>()
                                                                                                        {
                                                                                                            Succeeded = false,
                                                                                                            Errors = new string[] { "404" },
                                                                                                            Message = "No cards found "
                                                                                                        });

        }



    }
}
