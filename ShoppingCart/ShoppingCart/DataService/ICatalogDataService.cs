using habahabamall.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace habahabamall.DataService
{
    public interface ICatalogDataService
    {
        Task<List<Product>> GetProductBySubCategoryIdAsync(int subCategoryId);
        Task<Product> GetProductByIdAsync(int productId);
        Task<List<ProductReviewm>> GetReviewsAsync(int productId);
        Task<List<Payment>> GetPaymentOptionsAsync();
        Task<Status> AddRecentProduct(int userId, int productId);
        Task<List<Product>> GetRecentProductsAsync();
        Task<string> GetxmlProdttributes(int prodid, string attid);
    }
}