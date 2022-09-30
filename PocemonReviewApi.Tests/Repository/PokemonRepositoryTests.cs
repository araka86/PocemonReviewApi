using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PocemonReviewApi.Controllers;
using PocemonReviewApi.Data;
using PocemonReviewApi.Interface;
using PocemonReviewApi.Models;
using PocemonReviewApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocemonReviewApi.Tests.Repository
{



    public class PokemonRepositoryTests
    {



        private readonly IPokemonRepository _pokemonRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICatagoryRepository _catagoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public PokemonRepositoryTests()
        {
            _pokemonRepository = A.Fake<IPokemonRepository>();
            _reviewRepository = A.Fake<IReviewRepository>();
            _mapper = A.Fake<IMapper>();

        }









        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            if (await databaseContext.Pokemons.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Pokemons.Add(
                        new Pokemon()
                        {
                            Name = "Pikachu",
                            BirthDate = new DateTime(1993, 1, 1),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory()
                                {
                                    Category = new Category()
                                    {
                                        Name="Electric"
                                    }
                                }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review() {Title="Picachu",Text="text",Rating=5,
                                    Reviewer = new Reviewer(){FirstName = "Teddy", LastName = "Smiyh"}},
                                new Review(){ Title="Picachu" ,Text="text",Rating=5,
                                    Reviewer = new Reviewer(){ FirstName = "Taylor",LastName = "Jones"}},
                                new Review(){Title="Picachu",Text="text",Rating=1,
                                    Reviewer = new Reviewer(){FirstName = "Jissica", LastName = "McGregor"}},
                            }
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

     



        [Theory]
        [InlineData(1)]
        [InlineData("Pikachu")]
        public async void PokemonRepository_GetPokemon_ReturnPokemon(object data) 
        {
            //Arange
            var name = "Pikachu";
            var dbContext = await GetDatabaseContext();
            var pokemonRepository = new PokemonRepository(dbContext);

           

            //Act
            if (data.GetType() == typeof(int))
            {
               
                var result = pokemonRepository.GetPokemon(Convert.ToInt32(data));
                //Assert
                result.Should().NotBeNull();
                result.Should().BeOfType<Pokemon>();

            }
            else
            {
                var  result  = pokemonRepository.GetPokemon(Convert.ToString(data));
                //Assert
                result.Should().NotBeNull();
                result.Should().BeOfType<Pokemon>();
            } 
        }

        [Fact]
        public async void PokemonRepository_GetPokemonRating_ReturnDecimalBetweenOneAndTen()
        {
            //Arange

            var pokeId = 1;
            var dbContext = await GetDatabaseContext();
            var pokemonRepository = new PokemonRepository(dbContext);
            //Act
            var result = pokemonRepository.GetPokemonRating(pokeId);
            //Assert
            result.Should().NotBe(0);
            result.Should().BeInRange(0, 10);
        }




    }
}
