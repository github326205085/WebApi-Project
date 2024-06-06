using Entities;

namespace Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);

    }
}