using Microsoft.AspNetCore.Mvc;
using Shared;

namespace AuthServerAPI.Controllers
{

  

    public class CustomController  : Controller
    {
        [NonAction]
        public IActionResult Result<T>(ServiceResult<T> result) where T : class
        {
            if (result.Status == System.Net.HttpStatusCode.NoContent)
                return NoContent();

            //if (result.Status == System.Net.HttpStatusCode.Created)
            //    return Created(result.UrlCreated,result);


            return new ObjectResult(result)
            {
                StatusCode = result.Status.GetHashCode(),
            };
            
        }

        [NonAction]
        public IActionResult Result(ServiceResult result)
        {
            if (result.Status == System.Net.HttpStatusCode.NoContent)
                return NoContent();



            return new ObjectResult(result)
            {
                StatusCode = result.Status.GetHashCode(),
            };

        }
    }
}
