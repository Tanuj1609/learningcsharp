using ServiceContracts;
using Entities;
using ServiceContracts.DTO;
using RepositoryContracts;

namespace Services
{
    public class CountriesService : ICountriesService

    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;

        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {   //validation: country parameter can't be null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            //validation: country name can't be null
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest.CountryName));
            }

            //validation: country name cannot be duplicate
            if (_countriesRepository.GetCountryByCountryName(countryAddRequest.CountryName)!= null )
            {
                throw new ArgumentException("Given country name already exists");
            }
            //convert object from CountryAddrequest to country type 
            Country country = countryAddRequest.ToCountry();

            //generate country id
            country.CountryID = Guid.NewGuid();

            //Add country object into _countries
            _countriesRepository.AddCountry(country);
           
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return (_countriesRepository.GetAllCountries()).Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)

                return null;
            Country? country_response_from_list = _countriesRepository.GetCountryByCountryId(countryID.Value);

            if (country_response_from_list == null)
            { return null; }
            return country_response_from_list.ToCountryResponse();

        }
    }
}