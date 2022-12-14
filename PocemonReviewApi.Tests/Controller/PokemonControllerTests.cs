using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PocemonReviewApi.Controllers;
using PocemonReviewApi.Dto;
using PocemonReviewApi.Interface;
using PocemonReviewApi.Models;
using PocemonReviewApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocemonReviewApi.Tests.Controller
{
    public class PokemonControllerTests
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICatagoryRepository _catagoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public PokemonControllerTests()
        {
            _pokemonRepository = A.Fake<IPokemonRepository>();
            _reviewRepository = A.Fake<IReviewRepository>();
            _mapper = A.Fake<IMapper>();

        }
        [Fact]
        public void PokemonController_GetPokemons_ReturnOk()
        {
            //Arange
            var pokemons = A.Fake<ICollection<PokemonDto>>();
            var pokemonsList = A.Fake<List<PokemonDto>>();
            A.CallTo(() => _mapper.Map<List<PokemonDto>>(pokemons)).Returns(pokemonsList);
            var controller = new PokemonController(_pokemonRepository, _mapper, _reviewRepository);
            //Act

            var result = controller.GetPokemons();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }


        [Fact]
        public void PokemonController_CreatePokemon_ReturnOK()
        {
            //Arrange
            int ownerId = 1;
            int catId = 2;
            var pokemonMap = A.Fake<Pokemon>();
            var pokemon = A.Fake<Pokemon>();
            var pokemonCreate = A.Fake<PokemonDto>();
            var pokemons = A.Fake<ICollection<PokemonDto>>();
            var pokmonList = A.Fake<IList<PokemonDto>>();

            A.CallTo(() => _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate)).Returns(pokemon);               //check for exist
            A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreate)).Returns(pokemon);                                   //reverse model <----to------ map
            A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap)).Returns(true);             //create
            var controller = new PokemonController(_pokemonRepository,_mapper,_reviewRepository);

            //Act
            var result = controller.CreatePokemon(ownerId, catId, pokemonCreate);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public async void PokemonController_Delete_ReturnOK()
        {
            //Arange
            var pokemonId = 1;

            var pokemons = A.Fake<ICollection<Review>>();
            var pokemon = A.Fake<Pokemon>();

            A.CallTo(() => _pokemonRepository.PokemonExist(pokemonId)).Returns(true);
            A.CallTo(() => _reviewRepository.GetReviewsOfPokemon(pokemonId)).Returns(pokemons);
            A.CallTo(() => _pokemonRepository.GetPokemon(pokemonId)).Returns(pokemon);

            A.CallTo(() => _pokemonRepository.DeletePokemon(pokemon)).Returns(true);
            //Act
            var controller = new PokemonController(_pokemonRepository, _mapper, _reviewRepository);
            var result = controller.DeletePockemon(pokemonId);
            //  var result2 = pokemonRepository.DeletePokemon(pokemon);

            //Assert

            result.Should().NotBeNull();
        }

        [Fact]
        public async void PokemonController_UpdatePokemon_ReturnOK()
        {
            //Arange
            int ownerId =1;
            int catId = 1;
            int pokemonId =1;

            var pokemon = A.Fake<Pokemon>();
            var pokemonUpdate = A.Fake<PokemonDto>();
            pokemonUpdate.Id = ownerId;


            A.CallTo(() => _pokemonRepository.PokemonExist(pokemonId)).Returns(true);
            A.CallTo(() => _mapper.Map<Pokemon>(pokemonUpdate)).Returns(pokemon);
            A.CallTo(() => _pokemonRepository.UpdatePokemon(ownerId, catId, pokemon)).Returns(true);        //create
            var controller = new PokemonController(_pokemonRepository, _mapper, _reviewRepository);
            //Act


            var result = controller.UpdatePokemon(ownerId,catId,pokemonId, pokemonUpdate);
            // Assert
            result.Should().NotBeNull();
        }






    }
}
