using habahabamall.DependencyServices;
using habahabamall.Models;
using habahabamall.Views.AboutUs;
using habahabamall.Views.ContactUs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace habahabamall.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        // private string selectedCategory;

        public HomePage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
            MasterPage.ListView.ItemTapped += ListView_ItemTapped;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!(e.ItemData is HomePageMasterMenuItem item))
            {
                return;
            }

            Page page;
            if (item.Id == (int)MenuPage.Home)
            {
                page = new HomeTabbedPage();
                Detail = new NavigationPage(page);
            }

            else if (item.Id == (int)MenuPage.About)
            {
                page = new AboutUsSimplePage() { Title = item.Title };
                _ = Application.Current.MainPage.Navigation.PushAsync(page);
            }
            else if (item.Id == (int)MenuPage.Contact)
            {
                page = new ContactUsPage() { Title = item.Title };
                _ = Application.Current.MainPage.Navigation.PushAsync(page);
            }
            else
            {
                page = new HomeTabbedPage();
                Detail = new NavigationPage(page);
            }

            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Alert", "Are you sure you want to close?", "Yes", "No"))
                {
                    _ = base.OnBackButtonPressed();
                    ICloseApplication closeApplication = DependencyService.Get<ICloseApplication>();
                    if (closeApplication != null)
                    {
                        closeApplication.CloseApp();
                    }
                }
            });

            return true;
        }
    }
}