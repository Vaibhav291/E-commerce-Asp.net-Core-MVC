using Ecommerce.Models;

namespace Ecommerce.Data
{
    public interface IProductRepository
    {
            void Add(Product product);
            IEnumerable<Product> GetAll();
            Product GetById(int id);
            void Update(Product product);
            void Delete(int id);
            void Save();
    }
}
