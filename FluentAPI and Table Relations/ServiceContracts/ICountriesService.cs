using ServiceContracts.DTO;

namespace ServiceContracts
{   /// <summary>
///  contains business logic for manipulating country entity
/// </summary>
    public interface ICountriesService
    {   //THE TASK KEYWORD REPRESENTS THAT THE METHOD IS AWAITABLE FOR MAX CPU PERFORMANCE
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);

        Task<List<CountryResponse>> GetAllCountries();

        Task<CountryResponse?> GetCountryByCountryID(Guid? CountryID);
    }
}
