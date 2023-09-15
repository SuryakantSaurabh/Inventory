using TestingCrud.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingCrud.Model
{
    public class DbHelper
    {
        private EF_DataContext _context;
        public DbHelper(EF_DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: Retrieve a list of products.
        /// </summary>
        /// <returns>A list of product models.</returns>
        public List<ProductModel> GetProducts()
        {
            List<ProductModel> response = new List<ProductModel>();
            var dataList = _context.Products.ToList();
            dataList.ForEach(row => response.Add(new ProductModel()
            {
                ProductID = row.ProductID,
                ProductName = row.ProductName,
                Description = row.Description,
                UnitOfMeasure = row.UnitOfMeasure,
                Price = row.Price
            }));
            return response;
        }

        /// <summary>
        /// GET: Retrieve a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>A product model.</returns>
        public ProductModel GetProductById(int id)
        {
            var row = _context.Products.Where(d => d.ProductID == id).FirstOrDefault();
            if (row != null)
            {
                return new ProductModel()
                {
                    ProductID = row.ProductID,
                    ProductName = row.ProductName,
                    Description = row.Description,
                    UnitOfMeasure = row.UnitOfMeasure,
                    Price = row.Price
                };
            }
            return null;
        }

        /// <summary>
        /// POST/PUT: Create a new product or update an existing one.
        /// </summary>
        /// <param name="productModel">The product data to create or update.</param>
        public void SaveProduct(ProductModel productModel)
        {
            if (productModel.ProductID> 0)
            {
                // Update an existing product
                var product = _context.Products.Find(productModel.ProductID );
                if (product != null)
                {
                    product.ProductName = productModel.ProductName;
                    product.Description = productModel.Description;
                    product.UnitOfMeasure = productModel.UnitOfMeasure;
                    product.Price = productModel.Price;
                }
            }
            else
            {
                // Create a new product
                var product = new Product()
                {
                    ProductName = productModel.ProductName,
                    Description = productModel.Description,
                    UnitOfMeasure = productModel.UnitOfMeasure,
                    Price = productModel.Price
                };
                _context.Products.Add(product);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// DELETE: Delete a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
