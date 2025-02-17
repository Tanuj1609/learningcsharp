using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{   /// <summary>
/// person domain model class
/// </summary>
    public class Person
    {

        // THIS KEY ATTRIBUTE IS USED TO DECLARE THE COUNTRY ID AS KEY
        [Key]
        public Guid PersonID { get; set; }

        //LIMITING THE STRING LENGTH BY 40
        [StringLength(40)]
        public string? PersonName { get; set; }

        //LIMITING THE STRING LENGTH BY 40
        [StringLength(40)]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }

        //LIMITING THE STRING LENGTH BY 10
        [StringLength(10)]
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }

        //LIMITING THE STRING LENGTH BY 200
        [StringLength(200)]
        public string? Address { get; set; }

        public bool RecieveNewsLetters { get; set; }


        //HERE THE TIN STANDS FOR TAX IDENTIFICATION NUMBER
        //public string? TIN { get; set; }    
    }
}