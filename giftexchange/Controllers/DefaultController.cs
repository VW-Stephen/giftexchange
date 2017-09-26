using Microsoft.AspNetCore.Mvc;

namespace giftexchange.Controllers
{
    /// <summary>
    /// Controller for the main application frontend
    /// </summary>
    [Route("[controller]")]
    public class DefaultController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return new NoContentResult();
        }
    }
}
