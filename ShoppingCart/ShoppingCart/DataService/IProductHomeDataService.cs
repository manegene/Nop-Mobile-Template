using habahabamall.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace habahabamall.DataService
{
    public interface IProductHomeDataService
    {
        Task<List<Product>> GetOfferProductsAsync();
        Task<List<Banner>> GetBannersAsync();
    }
}