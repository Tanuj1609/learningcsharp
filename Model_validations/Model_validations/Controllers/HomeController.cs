using Microsoft.AspNetCore.Mvc;
using Model_validations.models;

namespace Model_validations.Controllers
{
    public class HomeController : Controller
    {
        [Route ("register")]
        public IActionResult Index(persons person)
        {
            return Content($"{ person}");
        }
    }
}
