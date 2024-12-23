using System.Net.NetworkInformation;
using System.Reflection;
using codeBlocks_and_expressions.Models;
using Microsoft.AspNetCore.Mvc;


namespace ViewsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home")]
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Asp.Net Core Demo App";
            return View();
            List<Person>? people = new List<Person>()
    {
        new Person() { Name = "John", dateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Person.Gender.Male},
        new Person() { Name = "Linda", dateOfBirth = DateTime.Parse("2005-01-09"), PersonGender = Person.Gender.Female},
        new Person() { Name = "Susan", dateOfBirth = DateTime.Parse("2008-07-12"), PersonGender = Person.Gender.Others}
    };
            ViewData["people"] = people ; 
            return View();
        }
    }
}
