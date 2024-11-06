using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OrderAPI.Controllers
{

    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        [HttpGet]

        public IActionResult Index()
        {
            var userName = HttpContext.User.Identity.Name;
            return Ok($"order controller user : {userName} , userId : {User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value}");
        }
    }
}
