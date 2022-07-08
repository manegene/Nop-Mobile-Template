using habahabamall.DataService;
using habahabamall.ViewModels.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.Forms
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleLoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleLoginPage" /> class.
        /// </summary>
        public SimpleLoginPage()
        {
            InitializeComponent();
            IUserDataService userDataService = App.MockDataService
                ? TypeLocator.Resolve<IUserDataService>()
                : DataService.TypeLocator.Resolve<IUserDataService>();
            IApiTokenService apiTokenService = App.MockDataService
                ? TypeLocator.Resolve<IApiTokenService>()
                : DataService.TypeLocator.Resolve<IApiTokenService>();

            BindingContext = new LoginPageViewModel(userDataService, apiTokenService);
        }
    }
}