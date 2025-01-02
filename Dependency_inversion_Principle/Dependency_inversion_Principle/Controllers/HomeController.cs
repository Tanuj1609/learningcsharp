using System.Security.Cryptography.X509Certificates;
using Services;
using ServiceContracts;
using Microsoft.AspNetCore.Mvc;


namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public HomeController(ICitiesService citiesService, IServiceScopeFactory serviceScopeFactory)
        {
            _citiesService = citiesService;
            _serviceScopeFactory = serviceScopeFactory;
        }
        [Route("/")]
        public IActionResult Index()
        {
           List<string> cities= _citiesService.GetCities();
            return View(cities);
        }
    }
}
