using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{/// <summary>
/// acts as a DTO for inserting a new person
/// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage ="Person name can not be blank")]
        public string? PersonName { get; set; }
        
        [Required(ErrorMessage = "Person email can not be blank")]
        [EmailAddress(ErrorMessage ="Email value should be a proper email")]
        
        [DataType(DataType.EmailAddress)] // DATA TYPE CAN BE SPECIFIED SO THAT THERE WON'T BE ANY NEED TO SPECIFY IN THE TAG HELPERS (ASP-FOR)
        public string? Email { get; set; }
       
        [Required(ErrorMessage = "DateofBirth of the person can not be left blank")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender of the person can not be blank")]
        public string? Gender { get; set; }
        [Required(ErrorMessage ="Please Select a country")]
        public Guid CountryID { get; set; }
        public string? Address { get; set; }
        public bool RecieveNewsLetters { get; set; }

        /// <summary>
        /// converts the current object of PersonAddRequest into a new object of person type
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            return new Person()
            {
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender?.ToString() ?? string.Empty,
                Address = Address,
                CountryID = CountryID,
                RecieveNewsLetters = RecieveNewsLetters
            };
        }
    }
}
