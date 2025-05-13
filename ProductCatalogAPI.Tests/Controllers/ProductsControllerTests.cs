using Moq;
using Xunit;
using ProductCatalogAPI.Application.Interfaces;
using ProductCatalogAPI.Core.Entities;
using ProductCatalogAPI.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogAPI.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductsController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkResult_WithProductList()
        {
            var mockProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 100 },
                new Product { Id = 2, Name = "Product 2", Price = 200 }
            };

            _mockService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(mockProducts);

            var result = await _controller.GetAllProducts();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal(2, ((List<Product>)products).Count);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            _mockService.Setup(s => s.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync((Product?)null);

            var result = await _controller.GetProductById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddProduct_ReturnsCreatedAtActionResult()
        {
            var product = new Product { Id = 1, Name = "Test", Price = 100 };

            var result = await _controller.AddProduct(product);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedProduct = Assert.IsType<Product>(createdAtResult.Value);
            Assert.Equal("Test", returnedProduct.Name);
        }
    }
}
