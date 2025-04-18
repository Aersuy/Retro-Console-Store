using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class ProductBL : IProductBL
    {   private readonly IError _error;
        private readonly ILog _log;
        private readonly IProductAPI _productAPI;

        public ProductBL(IError error, ILog log, IProductAPI productAPI)
        {
            _error = error;
            _log = log;
            _productAPI = productAPI;
        }
        public List<ProductModelBack> GetProductModelBacks()
        {    try
            {
                return _productAPI.GetAllProducts();

            }  catch(Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error getting product");
                throw;

            }
        
        }

    }
}
