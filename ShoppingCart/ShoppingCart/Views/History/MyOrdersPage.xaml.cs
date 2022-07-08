using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.ViewModels.History;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrdersPage : ContentPage
    {
        public MyOrdersPage()
        {
            InitializeComponent();
            FetchData();
        }

        public ObservableCollection<CartOrWishListProduct> Data { get; set; }

        private async void FetchData()
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                ICartDataService cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
                System.Collections.Generic.List<CartOrWishListProduct> orderedItem = await cartDataService.GetOrderedItemsAsync(App.UserId);
                if (orderedItem != null && orderedItem.Count > 0)
                {
                    (BindingContext as MyOrdersPageViewModel).MyOrders = new ObservableCollection<CartOrWishListProduct>(orderedItem);
                }
            }
            // if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            //  {
            // await App.Current.MainPage.DisplayAlert("Error!", "Network error! Connections are not available", "Cancel");
            //await Application.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            //  }
        }
    }
}