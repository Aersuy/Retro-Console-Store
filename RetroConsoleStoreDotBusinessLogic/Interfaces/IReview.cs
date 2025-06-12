using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Products;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface IReview
    {
        bool ReviewProduct(ReviewMessage message);
        List<ReviewT> GetReviewsForProudctBL(int productId);
        List<ReviewT> GetAllBL();
    }
}
