using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.Product;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface IProductBL
    {

        List<ProductModelBack> GetProductModelBacks();
        ProductModelBack GetProduct(int id);
        bool AddProduct(ProductModelBack product);
        bool UpdateProduct(ProductModelBack product);
        ProductModelBack GetProductById(int id);
        bool UpdateStock(int productId, int quantity);
        IEnumerable<ProductModelBack> Search(string searchTerm);
        bool DeleteProductBL(int id);

        IEnumerable<ProductModelBack> SortProductsBL(string sortBy, bool Ascending, IEnumerable<ProductModelBack> products);

    }
}
