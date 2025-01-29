using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{   /// <summary>
/// Represents the dto class that contains the person details to update
/// </summary>
    public class PersonUpdateRequest
    {
        /// <summary>
        /// acts as a DTO for inserting a new person
        /// </summary>
        [Required(ErrorMessage ="Person id can not be left blank")]
            public Guid PersonID { get; set; }
        
            [Required(ErrorMessage = "Person name can not be blank")]
            public string? PersonName { get; set; }

            [Required(ErrorMessage = "Person email can not be blank")]
            [EmailAddress(ErrorMessage = "Email value should be a proper email")]
            public string? Email { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string? Gender { get; set; }
            public Guid CountryID { get; set; }
            public string? Address { get; set; }
            public bool RecieveNewsLetters { get; set; }

            /// <summary>
            /// converts the current object of PersonAddRequest into a new object of person type
            /// </summary>
            /// <returns>The person object</returns>
            public Person ToPerson()
            {
                return new Person()
                {   PersonID = PersonID,
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
