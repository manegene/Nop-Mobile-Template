using habahabamall.Commands;
using habahabamall.DataService;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.ReviewsandRatings
{
    /// <summary>
    /// ViewModel for review page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ReviewPageViewModel : BaseViewModel
    {
        #region Field

        private DelegateCommand backButtonCommand;

        #endregion

        #region Constructor

        public ReviewPageViewModel(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
            UploadCommand = new Command<object>(OnUploadTapped);
            SubmitCommand = new Command<object>(OnSubmitTapped);
        }

        #endregion

        #region properties
        private readonly IUserDataService userDataService;
        private int prodId;
        private int rating;
        private string reviewText;
        private string title;
        #endregion

        #region public properties
        public int ProdId
        {
            get => prodId;
            set
            {
                prodId = value;
                OnPropertyChanged();

            }
        }
        public int UserId => App.UserId;

        public int Rating
        {
            get => rating;
            set
            {
                rating = value;
                OnPropertyChanged();
            }
        }
        public string ReviewText
        {
            get => reviewText;
            set
            {
                reviewText = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Command

        /// <summary>
        /// Gets or sets the value for upload command.
        /// </summary>
        public Command<object> UploadCommand { get; set; }

        /// <summary>
        /// Gets or sets the value for submit command.
        /// </summary>
        public Command<object> SubmitCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Upload button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void OnUploadTapped(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void OnSubmitTapped(object obj)
        {
            IsBusy = true;
            //await Application.Current.MainPage.DisplayAlert("Success", ProdId.ToString(), "", " ");

            _ = await userDataService.AddReview(UserId, ProdId, Rating, ReviewText, Title);
            IsBusy = false;

            _ = await Application.Current.MainPage.DisplayAlert("Success", "Your review has been successfully added", "", " ");

            _ = await Application.Current.MainPage.Navigation.PopAsync();
        }

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