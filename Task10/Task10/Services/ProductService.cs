using Task10.DTO;
using Task10.Interface;
using Task10.Model;

namespace Task10.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> repo;

        public ProductService(IRepository<Product> repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()   => await repo.GetAllAsync();

        public async Task<Product?> GetProductByIdAsync(Guid id)    => await repo.GetByIdAsync(id);

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Quantity = productDto.Quantity,
                Price = productDto.Price
            };

            await repo.AddAsync(product);
            await repo.SaveAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(Guid id, ProductDto dto)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return null;

            product.Name = dto.Name;
            product.Quantity = dto.Quantity;
            product.Price = dto.Price;

            repo.Update(product);
            await repo.SaveAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return false;

            repo.Delete(product);
            await repo.SaveAsync();
            return true;
        }
    }
}
