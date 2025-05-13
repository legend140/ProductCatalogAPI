using ProductCatalogAPI.Core.Entities;

namespace ProductCatalogAPI.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
    }
}
