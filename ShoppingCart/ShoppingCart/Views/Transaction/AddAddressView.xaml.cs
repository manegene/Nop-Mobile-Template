using habahabamall.DataService;
using habahabamall.ViewModels.Transaction;
using Syncfusion.XForms.ComboBox;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.Transaction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAddressView : ContentPage
    {
        public AddAddressView()
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

        private async void country_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var comb = new SfComboBox();
            await App.Current.MainPage.DisplayAlert("selc", "sel:" + comb.SelectedItem, "olk");
        }
    }
}