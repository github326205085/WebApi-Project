using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices CategoryServices;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryServices CategoryServices, IMapper mapper)
        {
            this.CategoryServices = CategoryServices;
            this.mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            List<Category> listCategories = (List<Category>)await CategoryServices.GetCategories();
            List < CategoryDTO > listCategoriesDTO= mapper.Map<List<Category>, List<CategoryDTO>>(listCategories);
            if (listCategories != null)
                return Ok(listCategoriesDTO);
            return NoContent();
        }

    }
}
