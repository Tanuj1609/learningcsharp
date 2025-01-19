using ServiceContracts.DTO;

namespace ServiceContracts
{   /// <summary>
///  contains business logic for manipulating country entity
/// </summary>
    public interface ICountriesService
    {
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        List<CountryResponse> GetAllCountries();

        CountryResponse? GetCountryByCountryID(Guid? CountryID);
    }
}
