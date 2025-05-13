using Xunit;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Infrastructure.Data;
using ProductCatalogAPI.Infrastructure.Repositories;
using ProductCatalogAPI.Core.Entities;

namespace ProductCatalogAPI.Tests
{
    public class ProductRepositoryTests
    {
        private readonly ProductDbContext _dbContext;
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _dbContext = new ProductDbContext(options);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _repository = new ProductRepository(_dbContext);
        }

        [Fact]
        public async Task AddAsync_AddsProductToDatabase()
        {
            var product = new Product { Name = "Test Product", Description = "Test Description", Price = 100 };

            await _repository.AddAsync(product);
            var result = await _repository.GetAllAsync();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
            _dbContext.Products.AddRange(new List<Product>
            {
                new Product { Name = "P1", Description = "D1", Price = 10 },
                new Product { Name = "P2", Description = "D2", Price = 20 }
            });
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            Assert.Equal(2, ((List<Product>)result).Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectProduct()
        {
            var product = new Product { Name = "P1", Description = "D1", Price = 10 };
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(product.Id);

            Assert.NotNull(result);
            Assert.Equal("P1", result.Name);
        }
    }
}
