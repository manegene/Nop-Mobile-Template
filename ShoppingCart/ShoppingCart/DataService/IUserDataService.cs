using habahabamall.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace habahabamall.DataService
{
    public interface IUserDataService
    {
        Task<Status> Login(User userInfo);
        Task<Status> SignUp(User userInfo);//string email, string password, string confirmpassword, string phone);
        Task<Status> ForgotPassword(User emailId);
        Task<List<Address>> GetAddresses(int userId);
        Task<Status> ContactUs(string name, string email, string message);
        Task<List<Address>> GetCountries();
        Task<List<Address>> GetProvinces();
        Task<List<UserCard>> GetUserCardsAsync(int userId);
        Task<Status> AddAddressDetails(string firstname, string lastname, string phone, string pcode, string city,
            string county, int countryid,int stateid, string address, string email, int userid);
        Task<Status> AddReview(int userid, int prodid, int rating, string reviewtext, string title);
        Task<Status> LipaNaMpesa(User user);
        Task<FlutterResponse> FlutterPay(FlutterPayModel flutterPayModel);

        Task<FlutterResponse> FlutterConfirm(int transactionId);

    }
}