using habahabamall.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace habahabamall.DataService
{
    public interface ICartDataService
    {
        Task<List<CartOrWishListProduct>> GetCartOrFavItemAsync(int userId, int carttype);
        Task<Status> AddOrderedItemAsync(int userid, int addressid, string paymentmode);
        Task<Status> AddCartOrFavItemAsync(int userId, int productId, string attributes, bool isfavorite, int carttype);
        Task<List<CartOrWishListProduct>> GetOrderedItemsAsync(int userId);
    }
}