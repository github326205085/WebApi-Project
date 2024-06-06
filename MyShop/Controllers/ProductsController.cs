using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;


namespace MyShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller
{
    private readonly IProductServices ProductServices;
    private readonly IMapper mapper;

    public ProductsController(IProductServices ProductServices, IMapper mapper)
    {
        this.ProductServices = ProductServices;
        this.mapper = mapper;
    }
    [HttpGet]



    public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery] int position, [FromQuery] int skip, [FromQuery] string? desc, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
    {
        List<Product> listProducts = await ProductServices.GetProducts(position, skip, desc, minPrice, maxPrice, categoryIds);

        if (listProducts != null)
        {
            var products = mapper.Map<List<ProductDTO>>(listProducts);
            return Ok(products);
        }

        return NoContent();
    }


}
