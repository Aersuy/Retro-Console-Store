using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.ProductsAPI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class ProductBL : ProductAPI, IProductBL
    {  
        public ProductBL(IError error, ILog log) : base(error, log)
        {

        }
        public List<ProductModelBack> GetProductModelBacks()
        {    
            return GetAllProductsAPI();
        }
        public ProductModelBack GetProduct(int id)
        {
           return GetProductByIdAPI(id);
        }
        public bool AddProduct(ProductModelBack product)
        {
            return AddProductAPI(product);
        }
        public bool UpdateProduct(ProductModelBack product)
        {
            return UpdateProductAPI(product);
        }
        public ProductModelBack GetProductById(int id)
        {
            return GetProductByIdAPI(id);
        }
        public bool UpdateStock(int productId, int quantity)
        {
            return UpdateStockAPI(productId, quantity);
        }
        public IEnumerable<ProductModelBack> Search(string searchTerm)
        {
            return SearchAPI(searchTerm);
        }
        public bool DeleteProductBL(int id)
        {
            return DeleteProductAPI(id);
        }
        public IEnumerable<ProductModelBack> SortProductsBL(string sortBy, bool Ascending, IEnumerable<ProductModelBack> products)
        {
            return SortProductsAPI(sortBy, Ascending, products);
        }

    }
}
