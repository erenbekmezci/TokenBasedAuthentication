using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiscountAPI.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class DiscountController : Controller
    {
        [HttpGet]

        public IActionResult Index()
        {
            var userName = HttpContext.User.Identity.Name;
            return Ok($"discount controller user : {userName} , userId : {User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value}");
        }
    }
}
