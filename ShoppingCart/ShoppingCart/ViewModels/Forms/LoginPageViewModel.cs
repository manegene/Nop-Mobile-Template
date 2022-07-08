using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.Views.Forms;
using habahabamall.Views.Home;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(IUserDataService userDataService, IApiTokenService apiTokenService)
        {
            this.userDataService = userDataService;
            this.apiTokenService = apiTokenService;
            LoginCommand = new DelegateCommand(LoginClicked);
            SignUpCommand = new DelegateCommand(SignUpClicked);
            ForgotPasswordCommand = new DelegateCommand(ForgotPasswordClicked);
            SocialMediaLoginCommand = new DelegateCommand(SocialLoggedIn);
        }


        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get => password;

            set
            {
                if (password == value)
                {
                    return;
                }

                password = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Fields
        public static Page page;

        private string password;
        private readonly IUserDataService userDataService;
        private readonly IApiTokenService apiTokenService;

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public DelegateCommand LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public DelegateCommand SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public DelegateCommand ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public DelegateCommand SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            try
            {

                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !IsInvalidEmail)
                {
                    IsBusy = true;
                    User loginUser = new User
                    {
                        EmailId = Email,
                        Password = password
                    };

                    if (!string.IsNullOrEmpty(App.AccessToken))
                    {
                        Status response = await userDataService.Login(loginUser);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                UserInfo userInfo = new UserInfo
                                {
                                    UserId = int.Parse(response.Message),
                                    UserName = Email,
                                    Name = response.Name,
                                    Password = Password,
                                    IsNewUser = false
                                };
                                if (!App.MockDataService)
                                {
                                    _ = await App.Database.ManegeUserDetail(userInfo);
                                }

                                App.UserId = int.Parse(response.Message);
                                App.EmailId = Email;
                                App.UserPassword = Password;
                                Application.Current.MainPage = new NavigationPage(new HomePage());
                                IsBusy = false;
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Message", "Login failed due to incorrect credentials " +
                                    "Click forgot password to set a new password", "OK");
                            }
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("information", "error getting session token.\n please check your credentials", "try again");

                    }

                    IsBusy = false;

                }
                else if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(password))
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter the email and password",
                        "OK");
                }
                else if (string.IsNullOrEmpty(Email))
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter the email address", "OK");
                }
                else if (string.IsNullOrEmpty(password) && !IsInvalidEmail)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter the password", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");

                await Application.Current.MainPage.DisplayAlert("Error", "unknown network error occured", "OK");
                IsBusy = false;
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

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SimpleForgotPasswordPage());
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}