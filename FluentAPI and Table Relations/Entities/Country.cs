using System.ComponentModel.DataAnnotations;

namespace Entities
{/// <summary>
/// this is the domain model for the country
/// </summary>
    public class Country
    {   
        // THIS KEY ATTRIBUTE IS USED TO DECLARE THE COUNTRY ID AS KEY
        [Key]
        public Guid CountryID { get; set; }
        public string? CountryName {  get; set; } 


        //ICOLLECTION IS CONSIDERED AS THE PARENT CLASS FOR LISTS, BASICALLY A RETURN DATA TYPE
        public virtual ICollection<Person>? Persons { get; set; }
    }
}
