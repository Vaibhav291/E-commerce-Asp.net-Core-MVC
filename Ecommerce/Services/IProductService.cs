using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IProductService
    {
            void AddProduct(Product product);
            IEnumerable<Product> GetAllProducts();
            Product GetProductById(int id);
            void UpdateProduct(Product product);
            void DeleteProduct(int id);
    }
}
