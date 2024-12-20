namespace codeBlocks_and_expressions.Models
{
    public class Person
    {
        public string? Name { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public Gender PersonGender { get; set; }

        public enum Gender
        {
            Male, Female, Others
        }
    }
}
