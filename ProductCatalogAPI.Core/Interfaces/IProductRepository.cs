using ProductCatalogAPI.Core.Entities;

namespace ProductCatalogAPI.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
    }
}
