using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.Product;
namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    internal interface IProductAPI
    {
        bool AddProduct(ProductModelBack Product);
        bool DeleteProduct(int Id);
        bool UpdateProduct(ProductModelBack Product);
        ProductModelBack GetProductById(int Id);
        IEnumerable<ProductModelBack> GetAllProducts();
        IEnumerable<ProductModelBack> GetProductsByCategory(string Category);   // Get products by category, category is name
        bool UpdateStock(int ProductID, int Quantity); // Update stock quantity
        IEnumerable<ProductModelBack> Seach(string SearchTerm); // Get products by brand

    }
}
