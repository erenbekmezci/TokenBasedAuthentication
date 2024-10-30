using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using Repositories.Products;
using Repositories.UnitOfWorks;
using Services.Dto;
using Shared;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService
    (
        IGenericRepository<Product> productRepository,
        IUnitOfWork unitOfWork

    ): IProductService 
    {
        public async Task<ServiceResult<ProductDto>> CreateAsync(ProductDto request)
        {
            var entity = ObjectMapper.Mapper.Map<Product>(request);
            await productRepository.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();
            var productDto = ObjectMapper.Mapper.Map<ProductDto>(entity);
            return ServiceResult<ProductDto>.SuccessCreated(productDto, $"api/products/{productDto.id}");
        }

        public async  Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await productRepository.GetByIdAsync(id);
            if(entity is null)
            {
                return ServiceResult.Fail("ürün bulunamdı", HttpStatusCode.NotFound);
            }
            productRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await productRepository.GetAllAsync();
            var productsDto = ObjectMapper.Mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.Success(productsDto);
        }

        public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult<ProductDto>.Fail("ürün bulunamdı", HttpStatusCode.NotFound);
            }
            return ServiceResult<ProductDto>.Success(ObjectMapper.Mapper.Map<ProductDto>(product) , HttpStatusCode.OK);

        }

        public async Task<ServiceResult> UpdateAsync(ProductDto request)
        {
            var product = await productRepository.GetByIdAsync(request.id);
            if (product is null)
            {
                return ServiceResult.Fail("ürün bulunamdı",HttpStatusCode.NotFound);
            }
            product = ObjectMapper.Mapper.Map(request, product);
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
            

        }
      

    }
}
