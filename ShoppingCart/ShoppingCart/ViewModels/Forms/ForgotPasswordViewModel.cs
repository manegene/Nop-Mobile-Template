using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.Views.Forms;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for forgot password page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ForgotPasswordViewModel : LoginViewModel
    {
        #region Private

        private readonly IUserDataService userDataService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordViewModel" /> class.
        /// </summary>
        public ForgotPasswordViewModel(IUserDataService userDataService)
        {
            this.userDataService = userDataService;

            SignUpCommand = new DelegateCommand(SignUpClicked);
            SendCommand = new DelegateCommand(SendClicked);
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Send button is clicked.
        /// </summary>
        public DelegateCommand SendCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public DelegateCommand SignUpCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the forgot password Send button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SendClicked(object obj)
        {

            if (!string.IsNullOrEmpty(Email) && !IsInvalidEmail)
            {
                IsBusy = true;

                var userobj = new User
                {
                    Email = Email
                };

                Models.Status status = await userDataService.ForgotPassword(userobj);
                if (status != null)
                {
                    if (status.Success)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new SimpleLoginPage());
                    }
                    else
                    {

                        await Application.Current.MainPage.DisplayAlert("Error", App.AccessToken, "OK");
                    }

                    IsBusy = false;
                }
            }
            else if (!IsInvalidEmail)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter the email address", "OK");
            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SimpleSignUpPage());
        }

        #endregion
    }
}