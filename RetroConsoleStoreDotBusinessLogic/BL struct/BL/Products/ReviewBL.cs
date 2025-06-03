using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.API.ProductsAPI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Product;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.BL.Products
{
    public class ReviewBL : ReviewAPI, IReview
    {
        public ReviewBL(IError error) : base(error)
        {
        }

        public bool ReviewProduct(ReviewMessage message)
        {
            return ReviewProductAPI(message);
        }
    }
}
