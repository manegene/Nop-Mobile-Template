using habahabamall.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.Home
{
    /// <summary>
    /// ViewModel for Home page master
    /// </summary>
    [Preserve(AllMembers = true)]
    public class HomePageMasterViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageMasterViewModel" /> class.
        /// </summary>
        public HomePageMasterViewModel()
        {
            //_ = _apitonservice.GetAccesToken(App.Usersavedinfo);
            UserEmail = App.EmailId;
            UserName = App.Name;
            MenuItems = new ObservableCollection<HomePageMasterMenuItem>(new[]
            {
                new HomePageMasterMenuItem {Id = 3, Title = "About Us", TitleIcon = "\ue714"},
                new HomePageMasterMenuItem {Id = 4, Title = "Contact Us", TitleIcon = "\ue71c"}

            });
        }
        //  public HomePageMasterViewModel(IApiTokenService apiTokenService)
        //  {
        //_apitonservice = apiTokenService;
        // _ = _apitonservice.GetAccesToken(App.Usersavedinfo);
        // }

        #endregion

        #region Private

        private string userName;

        private string userEmail;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the user email
        /// </summary>
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the user email
        /// </summary>
        public string UserEmail
        {
            get => userEmail;
            set
            {
                userEmail = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the menu items
        /// </summary>
        public ObservableCollection<HomePageMasterMenuItem> MenuItems { get; set; }
        // public IApiTokenService _apitonservice;

        #endregion
    }
}