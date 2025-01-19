using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace ServiceContracts.DTO
{   /// <summary>
/// Represents DTO class that is used as return type of most of methods of Persons service
/// </summary>
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }

        public string? Address { get; set; }
        public string? RecieveNewsLetters { get; set; }

        public double? Age { get; set; }
        /// <summary>
        /// compare the current object data with parameter object/// </summary>
        /// <param name="obj">The personresponse object to compare</param>
        /// <returns>True or false, indicating whether all person details are matched with the specified paramater object
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PersonResponse)) return false;
            PersonResponse person = (PersonResponse)obj;
            return this.PersonID == person.PersonID && PersonName == person.PersonName && Email == person.Email
                && DateOfBirth == person.DateOfBirth && Gender == person.Gender && CountryID == person.CountryID
                && Address == person.Address && RecieveNewsLetters == person.RecieveNewsLetters;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"PersonID : {PersonID}, Person Name: {PersonName}, Email : {Email}, " +
                $"Date of birth : {DateOfBirth?.ToString("dd MM YYYY")}, Gender : {Gender}, Country ID : {CountryID}," +
                $"Country : {Country}, Address : {Address}, Recieve News Letters : {RecieveNewsLetters}";

        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        { return new PersonUpdateRequest() {
            PersonID = PersonID,
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth ?? DateTime.MinValue,
            Gender = Gender,
            Address = Address,
            CountryID = CountryID ?? Guid.Empty,
            RecieveNewsLetters = Convert.ToBoolean(RecieveNewsLetters)
        }; 
        }
    }
    public static class PersonExtensions
    {/// <summary>
    /// An extension method to convert an object of Person class into PersonResponse class
    /// </summary>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                Address = person.Address,
                RecieveNewsLetters = person.RecieveNewsLetters.ToString(),
                CountryID = person.CountryID,
                Age = (person.DateOfBirth !=null)? Math.Round((DateTime.Now-person.DateOfBirth.Value).TotalDays/365.25):null
            };
        }
    }

}
