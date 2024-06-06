using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository productRepository;
        public ProductServices(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        public async Task<List<Product>> GetProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            return await productRepository.GetProducts( position,  skip,  desc, minPrice, maxPrice, categoryIds);
        }

      
    }
}
