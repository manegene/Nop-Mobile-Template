using habahabamall.DataService;
using habahabamall.ViewModels.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.Forms
{
    /// <summary>
    /// Page to retrieve the password forgotten.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleForgotPasswordPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleForgotPasswordPage" /> class.
        /// </summary>
        public SimpleForgotPasswordPage()
        {
            InitializeComponent();
            IUserDataService userDataService = App.MockDataService
                ? TypeLocator.Resolve<IUserDataService>()
                : DataService.TypeLocator.Resolve<IUserDataService>();
            BindingContext = new ForgotPasswordViewModel(userDataService);
        }
    }
}