using habahabamall.Models;
using System.Threading.Tasks;

namespace habahabamall.DataService
{
    public interface IApiTokenService
    {
        Task<UserTokenModel> GetAccesToken();
    }
}
