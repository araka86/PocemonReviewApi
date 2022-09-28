using AutoMapper;
using PocemonReviewApi.Data;
using PocemonReviewApi.Interface;
using PocemonReviewApi.Models;

namespace PocemonReviewApi.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CounntryExist(int countryid)
        {
            return _context.Countrys.Any(x => x.Id == countryid);
        }

        public bool CreateCountry(Country country)
        {
           _context.Countrys.Add(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countrys.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countrys.Where(x => x.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerid)
        {
            return _context.Owners.Where(o => o.Id == ownerid).Select(x => x.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryId)
        {
            return _context.Owners.Where(c => c.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }
    }
}
