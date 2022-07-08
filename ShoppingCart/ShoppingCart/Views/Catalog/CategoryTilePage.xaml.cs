using habahabamall.DataService;
using habahabamall.ViewModels.Catalog;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.Catalog
{
    /// <summary>
    /// The Category Tile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryTilePage
    {

        public CategoryTilePage()
        {
            InitializeComponent();
            ICategoryDataService categoryDataService = App.MockDataService
               ? TypeLocator.Resolve<ICategoryDataService>()
               : DataService.TypeLocator.Resolve<ICategoryDataService>();
            BindingContext = new CategoryPageViewModel(categoryDataService, "");
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryTilePage" /> class.
        /// </summary>
        public CategoryTilePage(string selectedCategory)
        {
            InitializeComponent();
            ICategoryDataService categoryDataService = App.MockDataService
                ? TypeLocator.Resolve<ICategoryDataService>()
                : DataService.TypeLocator.Resolve<ICategoryDataService>();
            BindingContext = new CategoryPageViewModel(categoryDataService, selectedCategory);
        }


    }
}