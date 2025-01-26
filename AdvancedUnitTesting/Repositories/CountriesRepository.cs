using RepositoryContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly ApplicationDbContext _db;
        public CountriesRepository(ApplicationDbContext db)
        {
            _db = db;
        }
    
        public Country AddCountry(Country country)
        {
            _db.Countries.Add(country); 
            _db.SaveChanges();
            return country;
        }

        public List<Country> GetAllCountries()
        {
            return _db.Countries.ToList();
        }

        public Country GetCountryByCountryId(Guid countryID)
        {
            return _db.Countries.FirstOrDefault(temp => temp.CountryID == countryID);  
        }

        public Country? GetCountryByCountryName(string countryName)
        {
            return _db.Countries.FirstOrDefault(temp => temp.CountryName == countryName);
        }
    }
}
