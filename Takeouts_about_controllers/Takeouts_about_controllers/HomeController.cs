using Microsoft.AspNetCore.Mvc;

namespace multiple_action_methods_in_controllers
{
    [Controller] //used as a convention, not manatory, if controller suffixed in class name
    public class Home
    {
        [Route("/")]
        [Route("Index")]
        public string Index()
        {
            return ("Hello from index");
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