using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace habahabamall.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTabbedPage : TabbedPage
    {
        public HomeTabbedPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}