using habahabamall.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace habahabamall.DataService
{
    public interface ICategoryDataService
    {
        Task<List<Category>> GetCategories();
        // Task<List<Category>> GetSubCategories(int categoryId);
    }
}