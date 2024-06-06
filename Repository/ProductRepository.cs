using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;
public class ProductRepository: IProductRepository
{
    private _326223617BookStoreContext bookStoreContext;
    public ProductRepository(_326223617BookStoreContext bookStoreContext)
    {
        this.bookStoreContext = bookStoreContext;
    }
    public async Task<List<Product>> GetProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds )
    {
        var query = bookStoreContext.Products.Where(product =>
        (desc == null ? (true) : (product.ProductName.Contains(desc)))
        && ((minPrice == null) ? (true) : (product.Price >= minPrice))
        && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
        && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
        .OrderBy(product=> product.Price);
        return await query.Include(p=>p.Category).ToListAsync();
    }
}
