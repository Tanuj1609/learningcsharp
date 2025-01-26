using Entities;

namespace RepositoryContracts
{/// <summary>
/// REPRESENTS DATA ACCESS LOGIC FOR MANAGING PERSON ENTITY
/// </summary>
    public interface ICountriesRepository
    {   /// <summary>
    /// ADDS A NEW COUNTRY OBJECT TO THE DATA STORE
    /// </summary>
    /// <param name="country">COUNTRY OBJECT TO ADD</param>
    /// <returns>RETURNS THE COUNTRY OBJECT AFTER ADDING IT TO THE DATA STORE</returns>
        Country AddCountry (Country country);    

       
        /// <summary>
        /// RETURNS ALL COUNTRIES IN THE DATA STORE
        /// </summary>
        /// <returns>ALL COUNTRIES FROM THE TABLE</returns>
        List<Country> GetAllCountries();

       
        /// <summary>
        /// RETURNS A COUNTRY OBJECT BASED ON THE GIVEN COUNTRY ID; OTHERWISE IT RETURNS NULL
        /// </summary>
        /// <param name="countryid">COUNTRYID TO BE SEARCHED</param>
        /// <returns>MATCHING COUNTRY OR NULL VALUE</returns>
        Country GetCountryByCountryId (Guid countryid);

        /// <summary>
        /// RETURNS A COUNTRY OBJECT BASED ON THE GIVEN COUNTRY NAME 
        /// </summary>
        /// <param name="countryName">COUNTRY NAME TO BE SEARCHED</param>
        /// <returns>MATCHING COUNTRY OR NULL VALUE</returns>
        Country? GetCountryByCountryName(string countryName);
    }
}
