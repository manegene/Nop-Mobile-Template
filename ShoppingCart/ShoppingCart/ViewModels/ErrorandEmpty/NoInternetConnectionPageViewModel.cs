using habahabamall.Commands;
using habahabamall.Views.Home;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.ErrorAndEmpty
{
    /// <summary>
    /// ViewModel for no internet connection page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class NoInternetConnectionPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="NoInternetConnectionPageViewModel" /> class.
        /// </summary>
        public NoInternetConnectionPageViewModel()
        {
            ImagePath = "NoInternet.svg";
            Header = "NO INTERNET";
            Content = "You must be connected to the internet to complete this action";
            TryAgainCommand = new DelegateCommand(TryAgain);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the TryAgain button is clicked.
        /// </summary>
        public DelegateCommand TryAgainCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Try again button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void TryAgain(object obj)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (Application.Current.MainPage.Navigation.NavigationStack.Count == 2)
                {

                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());

                    //await Application.Current.MainPage.DisplayAlert("We have", Application.Current.MainPage.Navigation.NavigationStack.Count.ToString(), "Ok");
                }
                else if (Application.Current.MainPage.Navigation.NavigationStack.Count > 2)
                {
                    _ = await Application.Current.MainPage.Navigation.PopAsync();
                }
            }


        }

        #endregion

        #region Fields

        private string imagePath;

        private string header;

        private string content;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ImagePath.
        /// </summary>
        public string ImagePath
        {
            get => imagePath;

            set
            {
                imagePath = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Header.
        /// </summary>
        public string Header
        {
            get => header;

            set
            {
                header = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Content.
        /// </summary>
        public string Content
        {
            get => content;

            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}