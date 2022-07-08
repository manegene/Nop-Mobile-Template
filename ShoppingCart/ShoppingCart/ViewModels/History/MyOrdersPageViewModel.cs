using habahabamall.Commands;
using habahabamall.Models;
using habahabamall.Models.History;
using habahabamall.Views.Detail;
using habahabamall.Views.ReviewsandRatings;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.History
{
    /// <summary>
    /// ViewModel for my orders page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class MyOrdersPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="MyOrdersPageViewModel" /> class.
        /// </summary>
        public MyOrdersPageViewModel()
        {
            if (MyOrders != null && MyOrders.Count == 0)
            {
                IsEmptyViewVisible = true;
            }
            else if (MyOrders != null && MyOrders.Count > 0)
            {
                IsEmptyViewVisible = false;
            }
        }

        #endregion

        #region Fields

        private ObservableCollection<Orders> orderDetails;

        private ObservableCollection<CartOrWishListProduct> myOrders = new ObservableCollection<CartOrWishListProduct>();

        private DelegateCommand itemSelectedCommand;

        private DelegateCommand backButtonCommand;

        private DelegateCommand reviewCommand;

        private bool isEmptyViewVisible;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of orders from json.
        /// </summary>
        [DataMember(Name = "orders")]
        public ObservableCollection<CartOrWishListProduct> MyOrders
        {
            get => myOrders;

            set
            {
                if (myOrders == value)
                {
                    return;
                }

                myOrders = value;

                if (myOrders != null && myOrders.Count == 0)
                {
                    IsEmptyViewVisible = true;
                }
                else if (myOrders != null && myOrders.Count > 0)
                {
                    IsEmptyViewVisible = false;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the order details in list.
        /// </summary>
        public ObservableCollection<Orders> OrderDetails
        {
            get => orderDetails;

            set
            {
                if (orderDetails == value)
                {
                    return;
                }

                orderDetails = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property whether view is visible.
        /// </summary>
        public bool IsEmptyViewVisible
        {
            get => isEmptyViewVisible;

            set
            {
                if (isEmptyViewVisible == value)
                {
                    return;
                }

                isEmptyViewVisible = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public DelegateCommand ItemSelectedCommand =>
            itemSelectedCommand ?? (itemSelectedCommand = new DelegateCommand(ItemSelected));

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public DelegateCommand ReviewCommand =>
            reviewCommand ?? (reviewCommand = new DelegateCommand(ReviewButtonClicked));

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private async void ItemSelected(object attachedObject)
        {
            if (attachedObject != null && attachedObject is CartOrWishListProduct cart && cart != null)
            {
                IsBusy = true;
                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(cart.ID));
                IsBusy = false;
            }
        }


        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            if (Application.Current.MainPage is NavigationPage && (Application.Current.MainPage as NavigationPage).CurrentPage is Views.Home.HomePage)
            {
                TabbedPage mainPage = ((((Application.Current.MainPage as NavigationPage).CurrentPage as FlyoutPage).Detail as NavigationPage).CurrentPage as TabbedPage);
                mainPage.CurrentPage = mainPage.Children[0];
            }
            else
            {
                _ = await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ReviewButtonClicked(object attachedObject)
        {
            if (attachedObject != null && attachedObject is CartOrWishListProduct cart && cart != null)
            {
                IsBusy = true;
                //await Application.Current.MainPage.DisplayAlert("prod", cart.Pname, "ok");
                await Application.Current.MainPage.Navigation.PushAsync(new ReviewPage(attachedObject));
                IsBusy = false;
            }
        }

        #endregion
    }
}