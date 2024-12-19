using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.Models
{
    public class Person
    {
        [Required(ErrorMessage = "{0} Name can't be empty or null")]
        [Display(Name ="Person Name")]
        [StringLength(40, MinimumLength =3, ErrorMessage ="Length is out of bounds")]
        [RegularExpression("^[A-Za-z .]$", ErrorMessage = "{Only alphabets accepted}")]
        public string? PersonName { get; set; }
        [EmailAddress(ErrorMessage ="Only email address is accepted")]
        [Required(ErrorMessage = "{0} can't be left empty")]
        public string? Email { get; set; }
        [Phone(ErrorMessage = "Enter 10 digit phone number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} can't be left empty")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "{0} can't be left empty")]
        [Compare("Password", ErrorMessage = "Password and confirm password are not same")]
        public string? ConfirmPassword { get; set; }
        [Range(0,999.99)]
        public double? Price { get; set; }

        public override string ToString()
        {
            return $"Person object - Person name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }
    }
}