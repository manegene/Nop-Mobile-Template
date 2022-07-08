using habahabamall.DataService;
using habahabamall.ViewModels.Catalog;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
//using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.Catalog
{
    /// <summary>
    /// Page to show the catalog list.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogListPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogListPage" /> class.
        /// </summary>
        public CatalogListPage(int selectedCategory)
        {
            InitializeComponent();
            ICatalogDataService catalogDataService = App.MockDataService
                ? TypeLocator.Resolve<ICatalogDataService>()
                : DataService.TypeLocator.Resolve<ICatalogDataService>();
            ICartDataService cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            
            BindingContext = new CatalogPageViewModel(catalogDataService, cartDataService,
                selectedCategory);
        }
    }
}