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
    //TODO : Add logging to the methods
    //TODO : Add error handling to the methods
    //TODO : Add validation to the methods
    
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
                    _log.ProductLog(Product, "Adding product to DB");
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
            using (var ctx = new UserContext())
            {
                try
                {
                    var Product = ctx.ProductTypes.Find(Id);
                    if (Product == null)
                    {
                        return false;
                    }
                    ctx.ProductTypes.Remove(Product);
                    ctx.SaveChanges();
                   // _log.ProductLog(Product, "Adding product to DB");
                    return true;
                }
                catch (Exception ex)
                {
                    _error.ErrorToDatabase(ex, "Problem with deleting product from the database");
                    return false;
                }
            }
            throw new NotImplementedException();
        }
        public bool UpdateProduct(ProductModelBack Product)
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    var ExistingProduct = ctx.ProductTypes.Find(Product.Id);
                    if (ExistingProduct == null)
                    {
                        return false;
                    }
                    ExistingProduct.Name = Product.Name;
                    ExistingProduct.Description = Product.Description;
                    ExistingProduct.ImagePath = Product.ImagePath;
                    ExistingProduct.Price = Product.Price;
                    ExistingProduct.StockQuantity = Product.StockQuantity;
                    ExistingProduct.Brand = Product.Brand;
                    ExistingProduct.TimeUpdated = DateTime.Now;
                    ExistingProduct.Status = Product.Status;
                    ExistingProduct.YearReleased = Product.YearReleased;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _error.ErrorToDatabase(ex, "Problem with updating product in the database");
                    return false;
                }
            }
            throw new NotImplementedException();
        }
        public ProductModelBack GetProductById(int Id)
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    var Product = ctx.ProductTypes.Find(Id);
                    if (Product == null)
                    {
                        return null;
                    }
                    return new ProductModelBack()
                    {
                        Id = Product.Id,
                        Name = Product.Name,
                        Description = Product.Description,
                        ImagePath = Product.ImagePath,
                        Price = Product.Price,
                        StockQuantity = Product.StockQuantity,
                        Brand = Product.Brand,
                        TimeCreated = Product.TimeCreated,
                        TimeUpdated = Product.TimeUpdated,
                        Status = Product.Status,
                        YearReleased = Product.YearReleased,
                        TotalSoldOnSite = Product.TotalSoldOnSite
                    };
                }
                catch (Exception ex)
                {
                    _error.ErrorToDatabase(ex, "Problem with getting product from the database");
                    return null;
                }
            }
            throw new NotImplementedException();
        }
        public IEnumerable<ProductModelBack> GetAllProducts()
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    var Products = ctx.ProductTypes.ToList();
                    return Products.Select(p => new ProductModelBack()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        ImagePath = p.ImagePath,
                        Price = p.Price,
                        StockQuantity = p.StockQuantity,
                        Brand = p.Brand,
                        TimeCreated = p.TimeCreated,
                        TimeUpdated = p.TimeUpdated,
                        Status = p.Status,
                        YearReleased = p.YearReleased,
                        TotalSoldOnSite = p.TotalSoldOnSite
                    }).ToList();
                }
                catch (Exception ex)
                {
                    _error.ErrorToDatabase(ex, "Problem with getting all products from the database");
                    return null;
                }
            }
            throw new NotImplementedException();
        }
       
        public bool UpdateStock(int ProductID, int Quantity)
        {
            using (var ctx = new UserContext())
            {
                try
                {
                    var Product = ctx.ProductTypes.Find(ProductID);
                    if (Product == null)
                    {
                        return false;
                    }
                    Product.StockQuantity += Quantity;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _error.ErrorToDatabase(ex, "Problem with updating stock in the database");
                    return false;
                }
            }
            throw new NotImplementedException();
        }
        public IEnumerable<ProductModelBack> Seach(string SearchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
