using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dto;

namespace AuthServerAPI.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : CustomController
    {
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto request)
        {
            return Result(await productService.CreateAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto request)
        {
            return Result(await productService.UpdateAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Result(await productService.GetAllListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Result(await productService.GetByIdAsync(id));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Result(await productService.DeleteAsync(id));
        }

    }
}
