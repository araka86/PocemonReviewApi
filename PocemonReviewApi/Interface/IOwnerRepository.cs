using PocemonReviewApi.Models;

namespace PocemonReviewApi.Interface
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfAPokemon(int pokeId);
        ICollection<Pokemon> GetPokemonByOwner(int ownerId);
        bool OwnerExist(int ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner ownerId);
        bool Save();
    }
}
