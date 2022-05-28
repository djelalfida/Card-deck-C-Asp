using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController: Controller
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistRepository artistRepo, IMapper mapper)
        {
            _artistRepo = artistRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetAllArtists()
        {
            return (_artistRepo.GetAllArtists(0) is IQueryable<Artist> artists) ?
                Ok(
                    artists.ProjectTo<ArtistReadDTO>(_mapper.ConfigurationProvider)
                    .ToList()
                    ) : NotFound(new Response<ArtistReadDTO>()
                    {
                        Succeeded = false,
                        Errors = new string[] { "404" },
                        Message = "No artists found "
                    });
        }


    }
}
