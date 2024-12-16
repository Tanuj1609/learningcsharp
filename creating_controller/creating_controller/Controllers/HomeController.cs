using Microsoft.AspNetCore.Mvc;

namespace creating_controller.Controllers
{
    public class HomeController
    {
        [Route ("sayhello")]
        public string method1()
        {
            return "hello thanos, this is method 1";
        }
    }
}
