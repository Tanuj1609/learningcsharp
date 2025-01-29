using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{   /// <summary>
/// REPRESENTS DATA ACCESS LOGIC FOR MANAGING PERSON ENTITY
/// </summary>
    public interface IPersonsRepository
    {
        /// <summary>
    /// ADDS A PERSON TO THE DATA STORE
    /// </summary>
    /// <param name="person">PERSON OBJECT TO BE ADDED</param>
    /// <returns>IT RETURNS THE PERSON OBJECT AFTER ADDING IT TO THE TABLE/returns>
        Person AddPerson(Person person);
        
        
        /// <summary>
        /// RETURNS ALL PERSONS IN THE DATA STORE
        /// </summary>
        /// <returns>LIST OF PERSON OBJECTS FROM TABLE</returns>
        List<Person> GetAllPersons();

        /// <summary>
        /// REURNS A PERSON OBJECT BASED ON THE GIVEN PERSON ID 
        /// </summary>
        /// <param name="personid">Personid(GUID) TO SEARCH</param>
        /// <returns>A PERSON OBJECT OR NULL</returns>
        List<Person> GetPersonByPersonID(Guid personid);

        /// <summary>
        /// RETURNS ALL PERSONS BASED ON THE GIVEN EXPRESSION
        /// </summary>
        /// <param name="predicate">LINQ EXPRESSION TO CHECK</param>
        /// <returns>ALL MATCHING PERSONS WITH THE GIVEN CONDITION</returns>
        List<Person> GetFilteredPersons(Expression<Func<Person,bool>>predicate);

        /// <summary>
        /// DELETES A PERSON OBJECT BASED ON THE PERSON ID
        /// </summary>
        /// <param name="personID">PERSON ID(guid) TO SEARCH</param>
        /// <returns>RETURNS TRUE, IF THE DELETION IS SUCCESSFULL; OTHERWISE IT RETURNS FALSE/returns>
        bool DeletePersonByPersonID(Guid personID);

        /// <summary>
        /// UPDATES A PERSON OBJECT(PERSON NAME AND OTHER DETAILS) BASED ON THE GIVEN PERSON ID
        /// </summary>
        /// <param name="person">PERSON OBJECT TO UPDATE</param>
        /// <returns>RETURNS THE UPDATED PERSON OBJECT</returns>
        Person UpdatePerson(Person person);
        
    }
}
