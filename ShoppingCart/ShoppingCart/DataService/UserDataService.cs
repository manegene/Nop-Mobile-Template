using AutoMapper;
using habahabamall.Models;
using Newtonsoft.Json;
using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace habahabamall.DataService
{
    /// <summary>
    /// Data service to load the data from database using Web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class UserDataService : IUserDataService
    {

        private readonly HttpClient httpClient;

        /// <summary>
        /// Creates an instance for the <see cref="UserDataService" /> class.
        /// </summary>
        public UserDataService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
                                        .Accept
                                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", App.AccessToken);

        }

        /// <summary>
        /// Login credential.
        /// </summary>
        public async Task<Status> Login(User loginuserDetails)
        {


            Status status = new Status();
            try
            {
                string userdetail = JsonConvert.SerializeObject(loginuserDetails);

                StringContent loginuser = new StringContent(userdetail, Encoding.UTF8, "application/json");
                UriBuilder uri = new UriBuilder($"{App.BaseUri}apiuser/login");

                HttpResponseMessage response = await httpClient.PostAsync(uri.ToString(), loginuser);
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    if (result != null)
                    {
                        status = JsonConvert.DeserializeObject<Status>(result);
                    }
                }

            }

            catch (Exception ex)
            {
                status.Success = false;
                status.Message = ex.Message;
            }

            return status;
        }

        /// <summary>
        /// This method is to create a new account.
        /// </summary>
        public async Task<Status> SignUp(User signupuser)
        {
            Status status = new Status();
            try
            {
                string serializedUser = JsonConvert.SerializeObject(signupuser);
                StringContent httpContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                UriBuilder uri = new UriBuilder($"{App.BaseUri}apiuser/signup");

                HttpResponseMessage response = await httpClient.PostAsync(uri.ToString(), httpContent);


                if (response != null && response.IsSuccessStatusCode)
                {

                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        status = JsonConvert.DeserializeObject<Status>(result);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                status.Success = false;
                status.Message = ex.Message;
            }
           

            return status;
        }

        /// <summary>
        /// This method is to get forgettable password.
        /// </summary>
        public async Task<Status> ForgotPassword(User emailId)
        {
            Status status = new Status();
            try
            {
                
                string jsonobj = JsonConvert.SerializeObject(emailId);
                StringContent userobjstrn = new StringContent(jsonobj, Encoding.UTF8, "application/json");

                UriBuilder uri = new UriBuilder($"{App.BaseUri}apiuser/forgot");
                HttpResponseMessage response = await httpClient.PostAsync(uri.ToString(), userobjstrn);
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        status = JsonConvert.DeserializeObject<Status>(result);
                    }
                }
            }

            catch (Exception ex)
            {
                status.Success = false;
                status.Message = ex.Message;
            }

            return status;
        }

        /// <summary>
        /// To get delivery address of logged user.
        /// </summary>
        public async Task<List<Address>> GetAddresses(int userId)
        {
            List<Address> Addresses = new List<Address>();
            try
            {
                var userobj = new
                {
                    Id = userId
                };
                string userseril = JsonConvert.SerializeObject(userobj);
                StringContent userobjcont = new StringContent(userseril, Encoding.UTF8, "application/json");
                UriBuilder uri = new UriBuilder($"{App.BaseUri}checkout/getshippingaddress");
                HttpResponseMessage response = await httpClient.PostAsync(uri.ToString(), userobjcont);
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        List<AddressEntity> userAddresses = JsonConvert.DeserializeObject<List<AddressEntity>>(result);
                        if (userAddresses != null)
                        {
                            Addresses = Mapper.Map<List<AddressEntity>, List<Address>>(userAddresses);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Addresses;
        }

        //get list of countries enable for shipping

        public async Task<List<Address>> GetCountries()
        {
            List<Address> Addresses = new List<Address>();
            try
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}checkout/listcountries");
                HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        List<AddressEntity> userAddresses = JsonConvert.DeserializeObject<List<AddressEntity>>(result);
                        if (userAddresses != null)
                        {
                            Addresses = Mapper.Map<List<AddressEntity>, List<Address>>(userAddresses);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }


            return Addresses;
        }
        public async Task<List<Address>> GetProvinces()
        {
            List<Address> Addresses = new List<Address>();
            try
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}checkout/listprovices");
                HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        List<AddressEntity> userAddresses = JsonConvert.DeserializeObject<List<AddressEntity>>(result);
                        if (userAddresses != null)
                        {
                            Addresses = Mapper.Map<List<AddressEntity>, List<Address>>(userAddresses);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }


            return Addresses;
        }

        /// <summary>
        /// To get added card details.
        /// not implemented
        /// </summary>
        public async Task<List<UserCard>> GetUserCardsAsync(int userId)
        {
            List<UserCard> UserCards = new List<UserCard>();
            try
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}users/card?userId={userId}");
                HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        List<UserCardEntity> userCards = JsonConvert.DeserializeObject<List<UserCardEntity>>(result);
                        if (userCards != null)
                        {
                            UserCards = Mapper.Map<List<UserCardEntity>, List<UserCard>>(userCards);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return UserCards;
        }
        //register user address
        public async Task<Status> AddAddressDetails(string firstname, string lastname, string phone, string pcode, string city,
            string county, int countryid,int stateid, string address, string email, int userid)
        {
            Status status = new Status();
            try
            {
                var addressobj = new
                {
                    firstname = firstname,
                    lastname = lastname,
                    mobileno = phone,
                    postalcode = pcode,
                    city = city,
                    county = county,
                    countryid = countryid,
                    stateproviceid=stateid,
                    address1 = address,
                    email = email,
                    userid = userid

                };
                string jsonuserobj = JsonConvert.SerializeObject(addressobj);
                StringContent userstrncont = new StringContent(jsonuserobj, Encoding.UTF8, "application/json");
                UriBuilder uri = new UriBuilder($"{App.BaseUri}checkout/addaddress");

                HttpResponseMessage response = await httpClient.PostAsync(uri.ToString(), userstrncont);


                if (response != null && response.IsSuccessStatusCode)
                {

                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        status = JsonConvert.DeserializeObject<Status>(result);
                    }
                }
            }

            catch (Exception ex)
            {
                status.Success = false;
                status.Message = ex.Message;
            }
            return status;

        }

        //Contact us by email
        public async Task<Status> ContactUs(string name, string email, string message)
        {
            Status status = new Status();
            try
            {
                var contobj = new
                {
                    name = name,
                    email = email,
                    message = message
                };
                string seruser = JsonConvert.SerializeObject(contobj);
                StringContent usercontstrn = new StringContent(seruser, Encoding.UTF8, "application/json");
                UriBuilder uri = new UriBuilder($"{App.BaseUri}apiuser/contactus");

                HttpResponseMessage response = await httpClient.PostAsync(uri.ToString(), usercontstrn);

                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        status = JsonConvert.DeserializeObject<Status>(result);
                    }
                }
            }

            catch (Exception ex)
            {
                status.Success = false;
                status.Message = ex.Message;
            }

            return status;
        }

        //add prod review 
        public async Task<Status> AddReview(int userid, int prodid, int rating, string reviewtext, string title)
        {
            Status status = new Status();
            try
            {
                var reviewobj = new
                {
                    userid = userid,
                    prodid = prodid,
                    rating = rating,
                    reviewtext = reviewtext,
                    title = title

                };
                string jsonobj = JsonConvert.SerializeObject(reviewobj);
                StringContent objstr = new StringContent(jsonobj, Encoding.UTF8, "application/json");

                UriBuilder uri = new UriBuilder($"{App.BaseUri}products/addreview");

                HttpResponseMessage response = await httpClient.PostAsync(uri.ToString(), objstr);

                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        status = JsonConvert.DeserializeObject<Status>(result);
                    }
                    //await App.Current.MainPage.DisplayAlert("ok", status.ToString(), "ol");
                }
            }

            catch (Exception ex)
            {
                status.Success = false;
                status.Message = ex.Message;
            }

            return status;
        }

        //Mpesa push
        public async Task<Status> LipaNaMpesa(User user)
        {
            HttpClient MpesahttpClient = new HttpClient();
            Status status = new Status();
            try
            {
                Status tokenstats = new Status();

                //first connect and and get user token from api
                UriBuilder Tokenuri = new UriBuilder($"https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials");
                MpesahttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "cFJZcjZ6anEwaThMMXp6d1FETUxwWkIzeVBDa2hNc2M6UmYyMkJmWm9nMHFRR2xWOQ==");
                HttpResponseMessage Tokenresponse = await MpesahttpClient.GetAsync(Tokenuri.ToString());
                if (Tokenresponse != null && Tokenresponse.IsSuccessStatusCode)
                {
                    string Tokenresult = Tokenresponse.Content.ReadAsStringAsync().Result;
                    if (Tokenresult != null)
                    {
                        tokenstats = JsonConvert.DeserializeObject<Status>(Tokenresult);

                        string Transacttoken = tokenstats.access_token;

                        //proceed with the payment now that we have a valid token

                        string serializedUser = JsonConvert.SerializeObject(user);
                        StringContent httpContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                        UriBuilder uri = new UriBuilder($"https://sandbox.safaricom.co.ke/mpesa/stkpush/v1/processrequest");
                        MpesahttpClient.DefaultRequestHeaders
                            .Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        MpesahttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Transacttoken);
                        HttpResponseMessage response = await MpesahttpClient.PostAsync(uri.ToString(), httpContent);
                        // await App.Current.MainPage.DisplayAlert("Oka", response.ToString(), "can");
                        if (response != null && response.IsSuccessStatusCode)
                        {
                            string result = response.Content.ReadAsStringAsync().Result;
                            if (result != null)
                            {
                                status = JsonConvert.DeserializeObject<Status>(result);
                            }
                        }
                        else
                        {
                            string result = response.Content.ReadAsStringAsync().Result;
                            if (result != null)
                            {
                                status = JsonConvert.DeserializeObject<Status>(result);
                            }
                        }

                    }
                }

            }
            catch (HttpRequestException)
            {
                //status.IsSuccess = false;
                // status.Message = ex.Message;
                await App.Current.MainPage.DisplayAlert("Internet error", "unknown error occured ", "Ok");

            }
            catch (Exception)
            {
                // status.IsSuccess = false;
                //status.Message = ex.Message;
                await App.Current.MainPage.DisplayAlert("Internet error", "A netowork error occured ", "Ok");

            }

            return status;
        }

        public async Task<FlutterResponse> FlutterPay(FlutterPayModel flutterPayModel)
        {
            HttpClient Flutterhttp = new HttpClient();
            FlutterResponse status = new FlutterResponse();
            try
            {
                string PayDetails = JsonConvert.SerializeObject(flutterPayModel);
                StringContent HttpContent = new StringContent(PayDetails, Encoding.UTF8, "application/json");

                UriBuilder PaymentURI = new UriBuilder($"https://api.flutterwave.com/v3/payments");
                Flutterhttp.DefaultRequestHeaders
                            .Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Flutterhttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.ScKey);

                HttpResponseMessage response = await Flutterhttp.PostAsync(PaymentURI.ToString(), HttpContent);

                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        status = JsonConvert.DeserializeObject<FlutterResponse>(result);
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        status = JsonConvert.DeserializeObject<FlutterResponse>(result);
                    }
                }

            }
            catch (Exception ex)
            {
                //to be updated on production
                await App.Current.MainPage.DisplayAlert("epay error!", ex.ToString(), "try again");
            }
            return status;
        }

        public async Task<FlutterResponse> FlutterConfirm(int transactionID)
        {
            HttpClient flutterconfirmhttp = new HttpClient();
            FlutterResponse flutterResponse = new FlutterResponse();
            if (transactionID > 0)
            {
                string pathvar = $"https://api.flutterwave.com/v3/transactions/" + transactionID + "/verify";
                UriBuilder confirmationURI = new UriBuilder(pathvar);
                flutterconfirmhttp.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                flutterconfirmhttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", App.ScKey);
                HttpResponseMessage response = await flutterconfirmhttp.GetAsync(confirmationURI.ToString());

                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        flutterResponse = JsonConvert.DeserializeObject<FlutterResponse>(result);
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        flutterResponse = JsonConvert.DeserializeObject<FlutterResponse>(result);
                    }
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("unknown error!", "error gettting transaction id. \n Contact habahabamall support for assistance", "okay");
            }
            return flutterResponse;

        }

    }
}