using habahabamall.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace habahabamall.DataService
{
    public interface IMyOrdersDataService
    {
        Task<List<Product>> GetMyOrderslistAsync(int userId);
        Task<Status> AddOrUpdateOrderlist(int userId, int productId, bool isFavorite);
    }
}