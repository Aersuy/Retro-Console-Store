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
        bool AddProduct(ProductModelBack Product); // Add product to the database
        bool DeleteProduct(int Id);  // Delete product from the database using the ID
        bool UpdateProduct(ProductModelBack Product); // Update product in the database, takes in product data
        // and completely replaces every field of the product with the new data
        ProductModelBack GetProductById(int Id); // Returns the product with the given ID, returns the model
        IEnumerable<ProductModelBack> GetAllProducts();    // Returns a IEnumerable of all products in the database
        // WARNING : using IEnumerable means that you cannot edit the data
        bool UpdateStock(int ProductID, int Quantity); // Update stock quantity
        IEnumerable<ProductModelBack> Seach(string SearchTerm); // Get products by brand
        // WARNING : Not yet implemented

    }
}
