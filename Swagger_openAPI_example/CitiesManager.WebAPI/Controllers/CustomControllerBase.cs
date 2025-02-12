using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}[controller]")] //FOR PASSING THE VERSION NUMBER DYNAMICALLY
    [ApiController]
    public class CustomControllerBase:ControllerBase
    {
    }
}