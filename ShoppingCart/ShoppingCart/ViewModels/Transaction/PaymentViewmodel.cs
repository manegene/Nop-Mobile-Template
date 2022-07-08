using habahabamall.Commands;
using habahabamall.Views.Home;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.Transaction
{
    /// <summary>
    /// ViewModel for Payment page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class PaymentViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="PaymentViewModel" /> class.
        /// </summary>
        public PaymentViewModel()
        {
            PaymentSuccessIcon = "PaymentSuccess.svg";
            PaymentFailureIcon = "PaymentFailure.svg";
            ContinueShoppingCommand = new DelegateCommand(ContinueShoppingClicked);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that will be executed when track order button is clicked.
        /// </summary>
        public DelegateCommand ContinueShoppingCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when track order button is clicked.
        /// </summary>
        private async void ContinueShoppingClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        #endregion

        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the payment success icon.
        /// </summary>
        public string PaymentSuccessIcon { get; set; }

        /// <summary>
        /// Gets or sets the payment failure icon.
        /// </summary>
        public string PaymentFailureIcon { get; set; }

        #endregion
    }
}