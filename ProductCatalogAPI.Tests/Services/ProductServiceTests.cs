using Moq;
using ProductCatalogAPI.Application.Services;
using ProductCatalogAPI.Core.Entities;
using ProductCatalogAPI.Core.Interfaces;
using Xunit;

namespace ProductCatalogAPI.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _mockProductRepository;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 100 },
                new Product { Id = 2, Name = "Product 2", Price = 150 }
            };

            _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            var result = await _productService.GetAllProductsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            var firstProduct = result.ElementAt(0);
            Assert.Equal("Product 1", firstProduct.Name);
            Assert.Equal(100, firstProduct.Price);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            var product = new Product { Id = 1, Name = "Product 1", Price = 100 };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            var result = await _productService.GetProductByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Product 1", result.Name);
            Assert.Equal(100, result.Price);
        }

        [Fact]
        public async Task AddProductAsync_ShouldCallAddProduct_WhenProductIsValid()
        {
            var product = new Product { Id = 1, Name = "Product 1", Price = 100 };

            _mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            await _productService.AddProductAsync(product);

            _mockProductRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
