using habahabamall.DataService;
using habahabamall.ViewModels.Detail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.Detail
{
    /// <summary>
    /// The Detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPage" /> class.
        /// </summary>
        public DetailPage(int selectedProduct)
        {
            InitializeComponent();
            ICatalogDataService catalogDataService = App.MockDataService
                ? TypeLocator.Resolve<ICatalogDataService>()
                : DataService.TypeLocator.Resolve<ICatalogDataService>();
            ICartDataService cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            BindingContext = new DetailPageViewModel(catalogDataService, cartDataService,
                selectedProduct);
        }

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            Rotator.ItemTemplate = width > height ? (DataTemplate)Resources["LandscapeTemplate"] : (DataTemplate)Resources["PortraitTemplate"];
        }

    }
}