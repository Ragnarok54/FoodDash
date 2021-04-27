using FoodDash.Web.DataAccess.Entities;
using FoodDash.Web.DataAccess.Repository.Interfaces;
using System.Collections.Generic;

namespace FoodDash.Web.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductType> _productTypeRepository;

        public ProductService(IRepository<Product> productRepository, IRepository<ProductType> productTypeRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
        }

        public Product GetProduct(int productId)
        {
            return _productRepository.Get(productId);
        }

        public void Add(Product product)
        {
            _productRepository.Add(product);
            _productRepository.SaveChanges();
        }

        public void Edit(Product product)
        {
            var existingProduct = _productRepository.Get(product.ProductId);

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.IsVegetarian = product.IsVegetarian;
            existingProduct.ProductTypeId = product.ProductTypeId;
            existingProduct.ServingSize = product.ServingSize;
            existingProduct.Photo = product.Photo;

            _productRepository.SaveChanges();
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
            return _productTypeRepository.GetAll();
        }
    }
}
