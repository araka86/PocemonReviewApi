using AutoMapper;
using PocemonReviewApi.Data;
using PocemonReviewApi.Interface;
using PocemonReviewApi.Models;
using System.Diagnostics.Metrics;

namespace PocemonReviewApi.Repository
{
    public class OwnerRepository : IOwnerRepository
    {

        private readonly DataContext _context;
       

        public OwnerRepository(DataContext context )
        {
            _context = context;
           
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(x => x.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _context.PokemonOwners.Where(x => x.Pokemon.Id == pokeId).Select(y => y.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
          return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(x => x.Owner.Id == ownerId).Select(y => y.Pokemon).ToList();
        }

        public bool OwnerExist(int ownerId)
        {
            return _context.Owners.Any(x => x.Id == ownerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {

            _context.Update(owner);
            return Save();
        }
    }
}
