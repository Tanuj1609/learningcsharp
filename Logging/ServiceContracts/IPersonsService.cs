using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{/// <summary>
/// contains business logic for manipulating the person entity
/// </summary>
    public interface IPersonsService
    {   /// <summary>
        /// Adds a new person into the list of persons
        /// /// </summary>
        /// <param name="personAddRequest">persons to add</param>
        /// <returns>Returns the same person details, along with the newly generated PersonID</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Returns all persons
        /// </summary>
        /// <returns>Returns a list of objects of personresponse type</returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Return the person object based on the given person id
        /// </summary>
        /// <param name="personID">Person id to be searched</param>
        /// <returns>Matching person object</returns>
        PersonResponse? GetPersonByPersonID(Guid? personID);



        /// <summary>
        /// Returns all the person objects that matches with the given search field and search string
        /// </summary>
        /// <param name="searchby">search field to search</param>
        /// <param name="searchstring">Search string to search</param>
        /// <returns>returns all the matching persons based on the given search field and the given seearch string</returns>
        List<PersonResponse> GetFilteredPersons(string searchby, string? searchstring);



        /// <summary>
        /// Returns the sorted list of persons  
        /// </summary>
        /// <param name="allPersons">Represents lists of persons to be sorted</param>
        /// <param name="sortBy">Name of the property(key), based on which the persons should be sorted</param>
        /// <param name="sortOrder">ASC or DESC</param>
        /// <returns>Returns sorted persons as  PersonResponse List</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);



        /// <summary>
        /// Updates the specified person details based on the given person ID
        /// </summary>
        /// <param name="personUpdateRequest">Person details to update, including person id</param>
        /// <returns>returns the person object after updation</returns>
        PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);


        /// <summary>
        /// Deletes a person based on the given person id
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>Returns true if the deletion is successfull and returns false if the deletion fails somehow</returns>
        bool DeletePerson(Guid? personID);
    }
}