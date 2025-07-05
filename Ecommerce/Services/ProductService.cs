using Ecommerce.Data;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct(Product product)
        {
            if(product.Price <= 0)
            {
                throw new System.Exception("Price must be greater than zero.");
            }

            _productRepository.Add(product);
            _productRepository.Save();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _productRepository.Save();
        }

        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
            _productRepository.Save();
        }
    }
}
