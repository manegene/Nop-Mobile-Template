
using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.ViewModels.Transaction;
using System.Collections.ObjectModel;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;


namespace habahabamall.Views.Transaction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Modal : ContentPage
    {

        #region properties
        private ObservableCollection<CartOrWishListProduct> ProceedCartDetails { get; set; }
        private string Address { get; set; }
        private int UserId { get; set; }

        private readonly CartDataService CallcartDataService;
        private readonly UserDataService CallUserDataService;

        #endregion
        public Modal(string weburl, ObservableCollection<CartOrWishListProduct> orderedItems, string addressid)
        {
            InitializeComponent();

            #region user specific order details
            Address = addressid;
            ProceedCartDetails = orderedItems;
            UserId = App.UserId;
            CallcartDataService = new CartDataService();
            CallUserDataService = new UserDataService();
            #endregion

            #region webview specic details
            webaddress.Source = weburl;
            title.Text = "habahabamall e-payment";
            #endregion

            IUserDataService userDataService = App.MockDataService
                ? TypeLocator.Resolve<IUserDataService>()
                : DataService.TypeLocator.Resolve<IUserDataService>();
            ICartDataService cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            ICatalogDataService catalogDataService = App.MockDataService
                ? TypeLocator.Resolve<ICatalogDataService>()
                : DataService.TypeLocator.Resolve<ICatalogDataService>();
            BindingContext = new CheckoutPageViewModel(userDataService, cartDataService, catalogDataService);
        }


        private async void webaddress_Navigating(object sender, WebNavigatingEventArgs e)
        {

            if (e.Url.Contains("status=cancelled"))
            {
                //user cancelled/transaction failed. Return user to order details page
                _ = await App.Current.MainPage.Navigation.PopModalAsync();
            }
            //user redirected successfully after payment is accepted
            else if (e.Url.Contains("status=successful"))
            {
                //payment is successful. User cannot go back with back button click
                e.Cancel = true;
                //IsBusy = true;

                //Get and covert the transaction id to an integer for use in verification endpoint
                string transId = HttpUtility.ParseQueryString(e.Url).Get("transaction_id");
                int verifyId = int.Parse(transId);

                //send the transasction ID for verification
                FlutterResponse verifyStatus = await CallUserDataService.FlutterConfirm(verifyId);

                //Transasction Id is verified succeessfully.Make the order and terminate the order process
                if (verifyStatus.Status == "success")
                {
                        //user cart items added to database
                        //api will handle the delete from cart
                        if (App.UserId > 0 && !string.IsNullOrEmpty(Address))
                        {
                           var success = await CallcartDataService.AddOrderedItemAsync(App.UserId, int.Parse(Address), paymentmode: "flutterwave");
                        if (success.Success == true)
                        {
                            IsBusy = false;
                            await Application.Current.MainPage.Navigation.PushAsync(new PaymentSuccessPage());
                        }
                        else if (success.Success == false)
                        {
                            IsBusy = false;
                            await App.Current.MainPage.DisplayAlert("error", "Error completing order.\n Please try again later" + success.Message, "ok");
                        }

                        }
                    

                }

            }
        }
    }
}