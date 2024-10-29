using Services.Dto;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductService
    {

        
        Task<ServiceResult<ProductDto>> GetByIdAsync(int id);
        Task<ServiceResult<ProductDto>> CreateAsync(ProductDto request);
        Task<ServiceResult> UpdateAsync(ProductDto request);
        Task<ServiceResult> UpdateStockAsync(ProductDto request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
       
    }
}
