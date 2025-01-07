using Microsoft.AspNetCore.Mvc;

namespace ConfigurationExample.Controllers
{
    public class HomeController : Controller
    {   
        //private field
        private readonly IConfiguration _configuration;

        //intialize the contructor
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Route("/")]
        public IActionResult Index()
        {   //use of the get section method for printing values of key value pairs
            WeatherApiOptions options = _configuration.GetSection("weatherapi").Get<WeatherApiOptions>();
            ViewBag.ClientId = _configuration.GetSection("weatherapi")["weatherapi:ClientId"];
            ViewBag.ClientSecret = options.ClientSecret;
            return View();
        }
    }
}
