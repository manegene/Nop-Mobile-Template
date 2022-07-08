using habahabamall.DataService;
using habahabamall.Mapping;
using habahabamall.Models;
using habahabamall.Views.ErrorAndEmpty;
using habahabamall.Views.Home;
using habahabamall.Views.Onboarding;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall
{
    public partial class App : Application
    {
        private static SQLiteDatabase database;
        private static DateTime datetime { get; } = DateTime.Now.ToUniversalTime();


        public App()
        {
            try
            {
                //initialize token service
                

                //Register Syncfusion license
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("your syncfusionlicense here");
                InitializeComponent();
                if (MockDataService)
                {
                    TypeLocator.Start();
                    MainPage = new NavigationPage(new OnBoardingAnimationPage());
                }
                else
                {
                    ListenNetworkChanges();
                    DataService.TypeLocator.Start();
                    MapperConfig.Config();
                    GetUserInfo();


                }
            }
            catch (Exception ex)
            {
                _ = Current.MainPage.DisplayAlert("error", ex.ToString(), "cancel");
            }
        }
        // Create the database connection as a singleton.
        public static SQLiteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Userinfo.db"));
                }
                return database;
            }
        }


        //flutter const params
        public static string ScKey { get; } = "Flutterwave license key here";
        public static string RedirecUrl { get; } = "https://{your store address here}/en/";

        public static string BaseImageUrl { get; } = "https://{your store address here}/images/thumbs/000/";

        //base category image urls address
        public static string BaseCategoryImageUrl { get; } = "https://{your store address here}/images";

        public static string BaseTokenurl { get; } = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={firebase webapikey here}"; //available in your firebase project setting

        public static string BaseUri = "https://{nop rest api address here}/api/";
        public static string AccessToken { get; set; }
        public static long Tokenexpiry { get; set; }


        public static bool MockDataService { get; } = false;

        public static int UserId { get; set; }
        public static string EmailId { get; set; }

        public static string UserPassword { get; set; }

        public static string Name { get; set; }

        public static Page page;


        private async void GetUserInfo()
        {
            //get saved user info
            UserInfo userInfo = Database.GetUserInfo().Result;

            //get saved token details
            UserTokenModel UserToken = Database.GetUserTokenInfo().Result;


            //User is already logged on. Get saved user datails and proceed to catalogue
            if (userInfo != null)
            {
                UserId = userInfo.UserId;
                EmailId = userInfo.UserName.Trim();
                Name = userInfo.Name;
                UserPassword = userInfo.Password;

                if (UserToken != null)
                {
                    DateTime expirydate = TimeConverter(UserToken.Expiresin);
                    int tokenTimeDiff = DateTime.Compare(datetime, expirydate);
                    if (tokenTimeDiff > -1)
                    {
                        await Task.Run(() => GetToken());

                    }
                    else
                    {
                        AccessToken = UserToken.IdToken;
                    }

                }

                //GetToken();
                MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                MainPage = new NavigationPage(new OnBoardingAnimationPage());
            }
        }
        private  static  void GetToken()
        {
            ApiTokenService apiTokenService = new ApiTokenService();

            try
            {
              Task.Run(() => apiTokenService.GetAccesToken());
            }

            catch (Exception)
            {
                throw new Exception("error getting accesstoken");
            }
        }

        public static void ListenNetworkChanges()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private static void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            CheckInternet();
        }


        private static void CheckInternet()
        {
            bool onErrorPage = false;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _ = Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());
            }
            else if (onErrorPage)
            {
                _ = Current.MainPage.Navigation.PopAsync();
            }
        }

        private static DateTime TimeConverter(long unixtime)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixtime).ToLocalTime();
            return dateTime;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            GetToken();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            GetToken();
        }
    }
}