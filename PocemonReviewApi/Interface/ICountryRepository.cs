using PocemonReviewApi.Models;

namespace PocemonReviewApi.Interface
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerid);

        ICollection<Owner> GetOwnersFromCountry(int countryId);
        bool CounntryExist(int id);
        bool CreateCountry(Country country);
        bool Save();
    }
}
