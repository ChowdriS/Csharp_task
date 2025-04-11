using Task10.DTO;
using Task10.Model;

namespace Task10.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task<Product?> UpdateProductAsync(Guid id, ProductDto productDto);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
