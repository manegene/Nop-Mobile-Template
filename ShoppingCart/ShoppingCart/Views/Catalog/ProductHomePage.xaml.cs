using habahabamall.DataService;
using habahabamall.ViewModels.Catalog;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.Catalog
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductHomePage : ContentPage
    {

        private static readonly IProductHomeDataService productHomeDataService = App.MockDataService
                ? TypeLocator.Resolve<IProductHomeDataService>()
                : DataService.TypeLocator.Resolve<IProductHomeDataService>();

        private static readonly ICatalogDataService catalogDataService = App.MockDataService
            ? TypeLocator.Resolve<ICatalogDataService>()
            : DataService.TypeLocator.Resolve<ICatalogDataService>();


        public ProductHomePage()
        {
            InitializeComponent();
            BindingContext = new ProductHomePageViewModel(productHomeDataService, catalogDataService);
        }

    }
}