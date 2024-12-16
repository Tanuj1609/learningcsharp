using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using JSON_result_CONTROLLERS.models;

namespace multiple_action_methods_in_controllers
{
    [Controller] //used as a convention, not manatory, if controller suffixed in class name
    public class Home : Controller
    {
        [Route("/")]
        [Route("Index")]
        public ContentResult Index()
        {
            //return Content("Hello from Thanos", "text/plain");
            return Content("<h1>Hello from Thanos</h1> <h2>hello from index</h2>", "text/html");
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person person = new Person()
            {
                id = Guid.NewGuid(),
                firstname = "Thanos",
                lastname = "Tew",
                age = 30
            };
            return Json(person);
        }

        [Route("Contact/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return ("Hello from Contact");
        }
    }
}