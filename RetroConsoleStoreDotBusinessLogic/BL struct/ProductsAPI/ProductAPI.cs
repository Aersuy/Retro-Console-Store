using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Products;
namespace RetroConsoleStoreDotBusinessLogic.BL_struct.ProductsAPI
{
    internal class ProductAPI : IProductAPI
    {
        readonly IError _error;
        readonly ILog _log;

        public ProductAPI(IError error, ILog log)
        {
            _error = error;
            _log = log;
        }
        public bool AddProduct(ProductModelBack Product)
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    var NewProduct = new ProductTypeT()
                    {
                        Id = Product.Id,
                        Name = Product.Name,
                        Description = Product.Description,
                        ImagePath = Product.ImagePath,
                        Price = Product.Price,
                        StockQuantity = Product.StockQuantity,
                        Brand = Product.Brand,
                        TimeCreated = DateTime.Now,
                        TimeUpdated = null,
                        Status = Product.Status,
                        YearReleased = Product.YearReleased,
                        TotalSoldOnSite = 0
                    };
                    ctx.ProductTypes.Add(NewProduct);
                    ctx.SaveChanges();
                    _log.ProductLog(Product);
                    return true;
                }
                catch (Exception ex)
                {
                    _error.ErrorToDatabase(ex, "Problem with adding product to the database");
                    return false;
                }
                
            }

         
        }
        public bool DeleteProduct(int Id)
        {
            throw new NotImplementedException();
        }
        public bool UpdateProduct(ProductModelBack Product)
        {
            throw new NotImplementedException();
        }
        public ProductModelBack GetProductById(int Id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ProductModelBack> GetAllProducts()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ProductModelBack> GetProductsByCategory(string Category)
        {
            throw new NotImplementedException();
        }
        public bool UpdateStock(int ProductID, int Quantity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ProductModelBack> Seach(string SearchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
