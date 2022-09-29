using PocemonReviewApi.Models;

namespace PocemonReviewApi.Interface
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int pokeid);
        bool PokemonExist(int pokeid);
        bool CreatePokemon(int ownerId,int categoryId,Pokemon pokemon);
        bool UpdatePokemon(int ownerId,int categoryId,Pokemon pokemon);
        bool DeletePokemon(Pokemon pokeid);
        bool Save();

    }
}
