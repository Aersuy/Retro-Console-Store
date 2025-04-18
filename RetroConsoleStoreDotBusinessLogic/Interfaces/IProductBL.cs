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
    }
}
