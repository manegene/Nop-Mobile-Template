using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.ViewModels.ReviewsandRatings;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = habahabamall.DependencyServices.TypeLocator;

namespace habahabamall.Views.ReviewsandRatings
{
    /// <summary>
    /// Page to get review from customer
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewPage
    {
        public ReviewPage(object obj)
        {
            InitializeComponent();
            IUserDataService userDataService = App.MockDataService
              ? TypeLocator.Resolve<IUserDataService>()
              : DataService.TypeLocator.Resolve<IUserDataService>();
            BindingContext = new ReviewPageViewModel(userDataService);

            productTitle.Text = (obj as CartOrWishListProduct).Pname;
            ProductImage.Source = (obj as CartOrWishListProduct).Previewimage.ToString();
            Pid.Text = (obj as CartOrWishListProduct).ID.ToString();



        }
    }
}