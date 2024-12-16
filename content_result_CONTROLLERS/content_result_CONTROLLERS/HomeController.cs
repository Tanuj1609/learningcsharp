using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace multiple_action_methods_in_controllers
{
    [Controller] //used as a convention, not manatory, if controller suffixed in class name
    public class Home:Controller
    {
        [Route("/")]
        [Route("Index")]
        public ContentResult Index()
        {
            //return Content("Hello from Thanos", "text/plain");
            return Content("<h1>Hello from Thanos</h1> <h2>hello from index</h2>", "text/html");
        }

        [Route("About")]
        public string About()
        {
            return ("Hello from About");
        }

        [Route("Contact/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return ("Hello from Contact");
        }
    }
}