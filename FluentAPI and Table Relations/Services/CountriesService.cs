using ServiceContracts;
using Entities;
using ServiceContracts.DTO;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CountriesService : ICountriesService

    {   private readonly PersonsDbContext _db;

        public CountriesService(PersonsDbContext personsDbContext)
        {
            _db = personsDbContext;
            
        }
        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {   //validation: country parameter can't be null
            if (countryAddRequest == null) { 
            throw new ArgumentNullException(nameof(countryAddRequest)); 
            }

            //validation: country name can't be null
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest.CountryName));
            }

            //validation: country name cannot be duplicate
            if (await _db.Countries.CountAsync(temp => temp.CountryName == countryAddRequest.CountryName) > 0) {
                throw new ArgumentException("Given country name already exists");
            }
            //convert object from CountryAddrequest to country type 
            Country country = countryAddRequest.ToCountry();

            //generate country id
            country.CountryID = Guid.NewGuid();
            
            //Add country object into _countries
            _db.Countries.Add(country); 
            await _db.SaveChangesAsync();
            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return await _db.Countries.Select(country  => country.ToCountryResponse()).ToListAsync();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
            
                return null;
            Country? country_response_from_list = await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryID == countryID);
            
            if (country_response_from_list == null)
                { return null; }
            return country_response_from_list.ToCountryResponse();
                
        }
    }
}
