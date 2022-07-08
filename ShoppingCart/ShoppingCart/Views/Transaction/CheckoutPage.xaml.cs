using habahabamall.DataService;
using habahabamall.ViewModels.Transaction;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.Transaction
{
    /// <summary>
    /// Page to show the Checkout details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutPage" /> class.
        /// </summary>
        public CheckoutPage()
        {
            InitializeComponent();
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
    }
}