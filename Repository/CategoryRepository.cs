using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private _326223617BookStoreContext bookStoreContext;
        public CategoryRepository(_326223617BookStoreContext bookStoreContext)
        {
            this.bookStoreContext = bookStoreContext;
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await bookStoreContext.Categories.ToListAsync();
        }
    }
}
