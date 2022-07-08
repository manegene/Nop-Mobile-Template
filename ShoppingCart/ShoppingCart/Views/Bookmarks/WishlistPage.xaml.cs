using habahabamall.DataService;
using habahabamall.ViewModels.Bookmarks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace habahabamall.Views.Bookmarks
{
    /// <summary>
    /// Page to show the wishlist.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishlistPage : ContentPage
    {
        public WishlistPage()
        {
            InitializeComponent();
            
            ICartDataService cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            BindingContext = new WishlistViewModel( cartDataService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is WishlistViewModel wishListVM)
            {
                IsBusy = true;
                Device.BeginInvokeOnMainThread(() => { wishListVM.FetchWishlist(); });
                IsBusy = false;
            }
        }
    }
}