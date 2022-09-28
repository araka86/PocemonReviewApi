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
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            // var pokemons = _pokemonRepository.GetPokemons();
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CounntryExist(countryId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);

        }

        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);

        }





        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto  countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country alredy exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var countryMap = _mapper.Map<Country>(countryCreate); //reverse model <----to------ map

            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Someting went wrong while saving");
                return StatusCode(500, ModelState);
            }
                return Ok("Successfully created!!!");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updatedcountry)
        {
            if (updatedcountry == null)
                return BadRequest(ModelState);

            if (countryId != updatedcountry.Id)
                return BadRequest(ModelState);

            if (!_countryRepository.CounntryExist(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var countryyMap = _mapper.Map<Country>(updatedcountry);
            if (!_countryRepository.UpdateCountry(countryyMap))
            {
                ModelState.AddModelError("", "Someting went wrong while updating country");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }









    }
}
