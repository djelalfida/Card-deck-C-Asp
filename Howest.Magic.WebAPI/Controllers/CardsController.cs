﻿using Microsoft.AspNetCore.Mvc;
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

                

                List<CardReadDTO> allCardsList = allCards
                               .ToFilteredList(filter.SetCode, filter.ArtistId, filter.RarityCode, filter.Name, filter.OriginalText)
                               .Sort(filter.Sort ?? string.Empty)
                               .ToPagedList(filter.PageNumber, filter.PageSize)
                               .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider).ToList();
                cachedResult = new PagedResponse<IEnumerable<CardReadDTO>>(
                           allCardsList,
                           filter.PageNumber,
                           filter.PageSize)
                {
                    TotalRecords = allCards.Count()
                };    
                foreach (CardReadDTO card in allCardsList)
                {
                    

                    if (string.IsNullOrEmpty(card.OriginalImageUrl) && card != null)
                    {
                            card.OriginalImageUrl = _cardRepo.GetOriginalImageUrl("7fef665c-36a1-5f7a-9299-cf8938708710");
                    }
                }


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

        // get all rarities
        [HttpGet("rarity")]
        public ActionResult<IEnumerable<RarityReadDTO>> GetAllRarities()
        {
            return (_cardRepo.GetRarities() is IQueryable<Rarity> rarities) ?
                Ok(
                    rarities.ProjectTo<RarityReadDTO>(_mapper.ConfigurationProvider)
                    .ToList()
                    ) : NotFound(new Response<RarityReadDTO>()
                    {
                        Succeeded = false,
                        Errors = new string[] { "404" },
                        Message = "No rarities found "
                    });
        }



    }
}
