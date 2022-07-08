using habahabamall.Commands;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.AboutUs
{
    /// <summary>
    /// ViewModel of AboutUs templates.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AboutUsViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="AboutUsViewModel" /> class.
        /// </summary>
        public AboutUsViewModel()
        {
            productDescription =
                "Habahabamall is an e-commerce platform for individuals/groups/institutions to buy and sell their merchandise to online buyers. Our core role is to provide a platform for trading activities, payment and delivery services between our vendors and buyers.";
            productIcon = "Icon.png";
            productVersion = "1.0";
        }

        #endregion

        #region Fields

        private string productDescription;

        private string productVersion;

        private string productIcon;

        private DelegateCommand backButtonCommand;

        #endregion

        #region Properties        

        /// <summary>
        /// Gets or sets the description of a product or a company.
        /// </summary>
        /// <value>The product description.</value>
        public string ProductDescription
        {
            get => productDescription;

            set
            {
                productDescription = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the product icon.
        /// </summary>
        /// <value>The product icon.</value>
        public string ProductIcon
        {
            get => productIcon;

            set
            {
                productIcon = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the product version.
        /// </summary>
        /// <value>The product version.</value>
        public string ProductVersion
        {
            get => productVersion;

            set
            {
                productVersion = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));


        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            _ = await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}