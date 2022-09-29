using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PocemonReviewApi.Dto;
using PocemonReviewApi.Interface;
using PocemonReviewApi.Models;
using PocemonReviewApi.Repository;

namespace PocemonReviewApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICatagoryRepository _catagoryRepository;
        private readonly IReviewRepository _reviewRepository;

        public IMapper _mapper { get; }

        public PokemonController(IPokemonRepository pokemonRepository, 
            IMapper mapper,
            IOwnerRepository ownerRepository,
            ICatagoryRepository catagoryRepository,
            IReviewRepository reviewRepository)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
            _ownerRepository = ownerRepository;
            _catagoryRepository = catagoryRepository;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
           // var pokemons = _pokemonRepository.GetPokemons();
            var pokemons = _mapper.Map<List<PokemonDto>>( _pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
                return BadRequest(pokemons);

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExist(pokeId)) 
            return NotFound();

            var pokemon = _mapper.Map<PokemonDto>( _pokemonRepository.GetPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);

        }



        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExist(pokeId))
                return NotFound();

            var rating = _pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemons = _pokemonRepository.GetPokemons()
                .Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (pokemons != null)
            {
                ModelState.AddModelError("", "Pokemon alredy exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate); //reverse model <----to------ map
            if (!_pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap))
            {
                ModelState.AddModelError("", "Someting went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created!!!");

        }



        [HttpPut("{pokemonId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon([FromQuery] int ownerId, [FromQuery] int catId, int pokemonId, [FromBody] PokemonDto updatedPokemon)
        {
            if (updatedPokemon == null)
                return BadRequest(ModelState);

            if (pokemonId != updatedPokemon.Id)
                return BadRequest(ModelState);

            if (!_pokemonRepository.PokemonExist(pokemonId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var pokemonMap = _mapper.Map<Pokemon>(updatedPokemon);

            if (!_pokemonRepository.UpdatePokemon(ownerId, catId, pokemonMap))
            {
                ModelState.AddModelError("", "Someting went wrong while updating pokemon");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{pokemonId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePockemon(int pokemonId)
        {
            if (!_pokemonRepository.PokemonExist(pokemonId))
                return NotFound();

            var reviewsToDelete = _reviewRepository.GetReviewsOfPokemon(pokemonId);
            var pokemonToDelete = _pokemonRepository.GetPokemon(pokemonId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Someting went wrong while deleting reviews");
            }

            if (!_pokemonRepository.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "Someting went wrong while deleting pokemon");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }






    }
}
