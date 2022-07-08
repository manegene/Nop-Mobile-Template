using habahabamall.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace habahabamall.DataService
{
    public class ApiTokenService : IApiTokenService
    {
        #region fields

        private readonly HttpClient httpClient;
        #endregion
        #region contructor
        public ApiTokenService()
        {

            httpClient = new HttpClient();
        }
        #endregion contructor

        public async Task<UserTokenModel> GetAccesToken()
        {
            UserTokenModel userUpdate;
            UserTokenModel Tokendetails = new UserTokenModel();
            FirebaseUser firebaseUser = new FirebaseUser();
            try
            {
                if (!string.IsNullOrEmpty(firebaseUser.email) || !string.IsNullOrEmpty(firebaseUser.password))
                {
                    string userdetails = JsonConvert.SerializeObject(firebaseUser);
                    StringContent httpcontent = new StringContent(userdetails, Encoding.UTF8, "application/json");
                    UriBuilder Tokenuri = new UriBuilder($"{App.BaseTokenurl}");
                    string result;

                    HttpResponseMessage response = await httpClient.PostAsync(Tokenuri.ToString(), httpcontent);

                    if (response != null && response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                        if (result != null)
                        {
                            Tokendetails = JsonConvert.DeserializeObject<UserTokenModel>(result);

                            if (Tokendetails != null)
                            {

                                //assign token after get to continue loading app quickly
                                App.AccessToken = Tokendetails.IdToken;

                                //await App.Current.MainPage.DisplayAlert("token 1", App.AccessToken, "okay");

                                //get token saved details
                                UserTokenModel saveduserinfo = App.Database.GetUserTokenInfo().Result;
                                userUpdate = saveduserinfo != null
                                    ? new UserTokenModel
                                    {
                                        ID = saveduserinfo.ID,
                                        IdToken = Tokendetails.IdToken,
                                        Expiresin = Tokendetails.Expiresin
                                    }
                                    : new UserTokenModel
                                    {
                                        IdToken = Tokendetails.IdToken,
                                        Expiresin = Tokendetails.Expiresin
                                    };

                                _ = await App.Database.ManageUserTokenDetail(userUpdate);
                            }
                        }
                    }
                    else if (response != null && !response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;

                        ErrorModel res = JsonConvert.DeserializeObject<ErrorModel>(result);
                        if (res != null)
                        {
                            await App.Current.MainPage.DisplayAlert("token error", res.Error.Message, "okay");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("error", e.ToString(), "ok");
            }
            return Tokendetails;

        }
    }
}
