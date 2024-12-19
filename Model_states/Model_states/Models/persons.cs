using System.ComponentModel.DataAnnotations;
namespace Model_validations.models
{
    public class persons
    {
        [Required]
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public double? Price { get; set; }

        public override string ToString()
        {
            return $"Person object- Person name: {PersonName},Email: {Email}, Phone: {Phone}," +
                $"Password: {Password}, Confirm Password: {ConfirmPassword}" +
                $"Price: {Price}";
        }


    }
}