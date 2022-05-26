using Howest.MagicCards.DAL.Models; // fix later
using Howest.MagicCards.DAL.Repositories; // fix later
using Howest.MagicCards.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardsController(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        // get all cards endpoint and automap to CardDTO
        [HttpGet]
        public ActionResult<IEnumerable<CardReadDTO>> GetAllCards()
        {
            var cards = _cardRepository.GetAllCards();
            var cardsDTO = _mapper.Map<IEnumerable<CardReadDTO>>(cards);
            return Ok(cardsDTO);
        }



    }
}
