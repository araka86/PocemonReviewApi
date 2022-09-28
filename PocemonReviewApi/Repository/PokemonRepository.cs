using PocemonReviewApi.Data;
using PocemonReviewApi.Interface;
using PocemonReviewApi.Models;

namespace PocemonReviewApi.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(x => x.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(x => x.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };
            _context.Add(pokemonOwner);

            var pocemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };
            _context.Add(pocemonCategory);
            _context.Add(pokemon);

            return Save();


        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(x => x.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeid)
        {
            var review = _context.Reviews.Where(x => x.Pokemon.Id == pokeid);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count()); //averge
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExist(int pokeid)
        {
            return _context.Pokemons.Any(p => p.Id == pokeid);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
