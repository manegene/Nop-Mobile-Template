using habahabamall.DataService;
using habahabamall.ViewModels.Bookmarks;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.Bookmarks
{
    /// <summary>
    /// Page to show the cart list.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartPage" /> class.
        /// </summary>
        public CartPage()
        {
            InitializeComponent();

            ICartDataService cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            BindingContext = new CartPageViewModel(cartDataService);
        }


    }
}