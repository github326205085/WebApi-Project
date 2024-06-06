using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Repository;

namespace TestProjectMyShop
{
    public class ProductRepositoryUnitTest
    {
        [Fact]
        public async Task TestGet_ReturnsProducts()
        {
            // Arrange
            var mockDbContext = new Mock<_326223617BookStoreContext>();
            var p = new List<Product> { new Product { ProductId = 1, ProductName = "aaa", Price = 10, Description = "aaa", CategoryId = 1, Image = "./aaa" } };

            mockDbContext.Setup(m => m.Products).ReturnsDbSet(p);
            var service = new ProductRepository(mockDbContext.Object);

            // Act
            var result = await service.GetProducts(0, 0, "aaa", 10, 100, new int?[] { 1 });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, p);
        }
    }
    }
