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
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
            LoginCommand = new DelegateCommand(LoginClicked);
            SignUpCommand = new DelegateCommand(SignUpClicked);
        }

        #endregion

        #region Fields

        private string firstname;
        private string lastname;
        private string phone;

        private string password;

        private string confirmPassword;
        private readonly IUserDataService userDataService;

        #endregion

        #region Property

        public string LastName
        {
            get => lastname;

            set
            {
                if (lastname == value)
                {
                    return;
                }

                lastname = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string FirstName
        {
            get => firstname;

            set
            {
                if (firstname == value)
                {
                    return;
                }

                firstname = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
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

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up
        /// page.
        /// </summary>
        public string ConfirmPassword
        {
            get => confirmPassword;

            set
            {
                if (confirmPassword == value)
                {
                    return;
                }

                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => phone;
            set
            {
                if (phone == value)
                {
                    return;
                }

                phone = value;
                OnPropertyChanged();
            }
        }

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

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SimpleLoginPage());
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) ||
                    string.IsNullOrEmpty(ConfirmPassword) || string.IsNullOrEmpty(Phone))
                {
                    await Application.Current.MainPage.DisplayAlert("signup error", "Please fill all the fields and try again", "OK");
                }
                else if (!IsInvalidEmail)
                {
                    bool isValidPassword = string.Equals(Password, ConfirmPassword);
                    if (isValidPassword)
                    {
                        IsBusy = true;
                        User RegUserDetails = new User
                        {
                            EmailId = Email,
                            Password = Password,
                            Phone = Phone,
                            FirstName = FirstName,
                            LastName = LastName

                        };

                        Status response = await userDataService.SignUp(RegUserDetails);

                        if (response.Success)
                        {
                            Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Un expected error occured. Contact admin@habahabamall.com for assistance", "OK");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Password is mismatched", "OK");
                    }
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        #endregion
    }
}