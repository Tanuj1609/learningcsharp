using Microsoft.AspNetCore.Mvc;

namespace viewimports.cshtml_in_RV.Controllers
{
    public class ProductsController : Controller
    {   [Route("all-products")]
        public IActionResult All()
        {
            return View();
        }
    }
}
