using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("book")]
        public IActionResult Index()
        {
            //Book id should be applied
            if (!Request.Query.ContainsKey("bookid"))
            {
                return BadRequest("book id is not supplied");
            }

            //Book id can't be empty
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                return BadRequest("Book id can't be null or empty");
            }

            //Book id should be between 1 to 1000
            int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
            if (bookId <= 0)
            {
                return BadRequest("Book can't be equal to or less than zero");
            }
            if (bookId > 1000)
            {
                return NotFound("Book id can't be greater than 1000");
            }

            //isloggedin should be true
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                return Unauthorized("user must be authenticated");
            }

            return File("/dummypdf.pdf", "application/pdf");
        }
    }
}