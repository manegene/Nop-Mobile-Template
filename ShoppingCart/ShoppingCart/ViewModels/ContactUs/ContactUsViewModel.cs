using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.Views.Home;
using Syncfusion.SfMaps.XForms;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.ContactUs
{
    /// <summary>
    /// ViewModel for contact us page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ContactUsViewModel : BaseViewModel
    {
        #region  Fields

        private DelegateCommand backButtonCommand;
        private readonly IUserDataService userDataService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsViewModel" /> class.
        /// </summary>
        public ContactUsViewModel(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
            SendCommand = new DelegateCommand(Send);
            CustomMarkers = new ObservableCollection<MapMarker>();
            GetPinLocation();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Send button is clicked.
        /// </summary>
        public DelegateCommand SendCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));


        #endregion

        #region Fields

        private ObservableCollection<MapMarker> customMarkers;

        private Point geoCoordinate;

        private string name;

        private string email;

        private string message;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CustomMarkers collection.
        /// </summary>
        public ObservableCollection<MapMarker> CustomMarkers
        {
            get => customMarkers;

            set
            {
                customMarkers = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the geo coordinate.
        /// </summary>
        public Point GeoCoordinate
        {
            get => geoCoordinate;

            set
            {
                geoCoordinate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods      

        /// <summary>
        /// Invoked when the send button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void Send(object obj)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Message))
            {
                await Application.Current.MainPage.DisplayAlert("message error", "all message fields are mandatory", "Ok");
            }
            else if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Message))
            {
                IsBusy = true;
                _ = await userDataService.ContactUs(Name, Email, Message);
                IsBusy = false;

                await Application.Current.MainPage.DisplayAlert("Message", "Your query was sent successfully",
                    "Go to Home");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }

        }

        /// <summary>
        /// This method is for getting the pin location detail.
        /// </summary>
        private void GetPinLocation()
        {
            CustomMarkers.Add(
                new LocationMarker
                {
                    PinImage = "Pin.png",
                    Header = "habahabamall",
                    Address = "Kenya",
                    EmailId = "about@habahabamall.com",
                    PhoneNumber = "+254-20-440-1322",
                    CloseImage = "Close.png",
                    Latitude = "-1.2835",
                    Longitude = "36.8236"
                });

            foreach (MapMarker marker in CustomMarkers)
            {
                GeoCoordinate = new Point(Convert.ToDouble(marker.Latitude), Convert.ToDouble(marker.Longitude));
            }
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