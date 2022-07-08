using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.Views.Detail;
using habahabamall.Views.ErrorAndEmpty;
using habahabamall.Views.Forms;
using habahabamall.Views.Home;
using habahabamall.Views.Transaction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.Bookmarks
{
    /// <summary>
    /// ViewModel for cart page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CartPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CartPageViewModel" /> class.
        /// </summary>
        public CartPageViewModel(ICartDataService cartDataService)
        {
            this.cartDataService = cartDataService;
            //Device.BeginInvokeOnMainThread(() =>
            // {
            //     FetchCartProducts();
            // });
        }

        #endregion

        #region Fields
        private DelegateCommand itemSelectedCommand;

        private ObservableCollection<CartOrWishListProduct> userCarts;
        private string storeCurrency;
        private double totalPrice;
        private bool isDiscountPositive;

        private double discountPrice;

        private double discountPercent;

        private double percent;

        private bool isEmptyViewVisible;

        private DelegateCommand placeOrderCommand;

        private Command removeCommand;


        private DelegateCommand backButtonCommand;

        private readonly ICartDataService cartDataService;

        #endregion

        #region Public properties
        public bool IsDiscountPositive
        {
            get => isDiscountPositive;
            set
            {
                isDiscountPositive = value;
                OnPropertyChanged();
            }
        }
        public string StoreCurrency
        {
            get => storeCurrency;
            set
            {
                storeCurrency = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
        public ObservableCollection<CartOrWishListProduct> UserCarts
        {
            get => userCarts;

            set
            {
                if (userCarts == value)
                {
                    return;
                }

                userCarts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays the total price.
        /// </summary>
        public double TotalPrice
        {
            get => totalPrice;

            set
            {
                if (totalPrice == value)
                {
                    return;
                }

                totalPrice = value;
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

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays total discount price.
        /// </summary>
        public double DiscountPrice
        {
            get => discountPrice;

            set
            {
                if (discountPrice == value)
                {
                    return;
                }

                discountPrice = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays discount.
        /// </summary>
        public double DiscountPercent
        {
            get => discountPercent;

            set
            {
                if (discountPercent == value)
                {
                    return;
                }

                discountPercent = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays percent.
        /// </summary>
        public double Percent
        {
            get => percent;
            set
            {
                if (percent == value)
                {
                    return;
                }

                percent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command
        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public DelegateCommand ItemSelectedCommand =>
            itemSelectedCommand ?? (itemSelectedCommand = new DelegateCommand(ItemSelected));

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public DelegateCommand PlaceOrderCommand =>
            placeOrderCommand ?? (placeOrderCommand = new DelegateCommand(PlaceOrderClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the Remove button is clicked.
        /// </summary>
        public Command RemoveCommand =>
            removeCommand ?? (removeCommand = new Command(RemoveClicked));


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
            if (attachedObject != null && attachedObject is CartOrWishListProduct product && product != null)
            {
                IsBusy = true;
                int prodid = product.ID;

                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(prodid));
                IsBusy = false;
            }
        }

        /// <summary>
        /// This method is for getting the cart items
        /// </summary>
        public async void FetchCartProducts()
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {
                    if (App.UserId > 0)
                    {
                        List<CartOrWishListProduct> cartProducts = await cartDataService.GetCartOrFavItemAsync(App.UserId, carttype: 1);
                        if (cartProducts != null && cartProducts.Count > 0)
                        {
                            IsEmptyViewVisible = false;
                            UserCarts = new ObservableCollection<CartOrWishListProduct>(cartProducts);

                            //get the store currency value
                            if (userCarts != null)
                                StoreCurrency =UserCarts.Select(curr => curr.Currency).FirstOrDefault();

                            foreach (CartOrWishListProduct cart in UserCarts)
                            {
                                if (cart.DiscountAppliedToProducts > 0)
                                {
                                    cart.IsDiscountPositive = true;
                                }

                                cart.Quantities = new List<object> { "1", "2", "3" };
                                UpdatePrice();
                            }

                        }
                        else
                        {
                            if (Application.Current.MainPage is NavigationPage &&
                                (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
                            {
                                IsEmptyViewVisible = true;
                            }
                            else
                            {
                                await Application.Current.MainPage.Navigation.PushAsync(new EmptyCartPage(true));
                            }
                        }
                    }
                    else
                    {

                        bool result = await Application.Current.MainPage.DisplayAlert("Message",
                            "Please login to view your cart items", "OK", "CANCEL");
                        if (result)
                        {
                            Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                        }
                    }
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Unknown error occured", "OK");
                }
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void PlaceOrderClicked(object obj)
        {


            if (App.UserId >0)
            {
                try
                {

                    
                    await Application.Current.MainPage.Navigation.PushAsync(new CheckoutPage());

                }
                catch (Exception)
                {
                   var redireclogin= await Application.Current.MainPage.DisplayAlert("sorry", "Please login to complete the order", "okay","cancel");
                    if (redireclogin)
                        await App.Current.MainPage.Navigation.PushAsync(new SimpleLoginPage());
                }
            }

        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void RemoveClicked(object obj)
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {
                    if (obj != null && obj is CartOrWishListProduct userCart && userCart != null)
                    {
                        Status status = await cartDataService.AddCartOrFavItemAsync(App.UserId, userCart.ID, attributes: "", isfavorite: false, carttype: 1);
                        if (status != null && status.Success)
                        {
                            _ = UserCarts.Remove(userCart);
                            UpdatePrice();

                            if (userCarts.Count == 0)
                            {
                                if (Application.Current.MainPage is NavigationPage &&
                                    (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
                                {
                                    IsEmptyViewVisible = true;
                                }
                                else
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new EmptyCartPage(true));
                                }
                            }
                            else
                            {
                                IsEmptyViewVisible = false;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "unknow error occured", "OK");
                }
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
        }

        /// <summary>
        /// Invoked when the quantity is selected.
        /// </summary>
        /// <param name="selectedItem">The Object</param>

        /*  if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
          {
              await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

          }*/


        /// <summary>
        /// This method is used to update the price amount.
        /// </summary>
        public void UpdatePrice()
        {
            ResetPriceValue();

            if (UserCarts != null && UserCarts.Count > 0)
            {
                foreach (CartOrWishListProduct cartDetail in UserCarts)
                {
                    if (cartDetail.Quantity == 0)
                    {
                        cartDetail.Quantity = 1;
                    }

                    TotalPrice += cartDetail.OldPrice * cartDetail.Quantity;
                    DiscountPrice += cartDetail.Price * cartDetail.Quantity;
                    Percent += cartDetail.DiscountPrice;
                }

                DiscountPercent = Percent > 0 ? percent / UserCarts.Count : 0;
                if (DiscountPercent > 0)
                {
                    IsDiscountPositive = true;
                }
            }
        }

        /// <summary>
        /// This method is used to reset the price amount.
        /// </summary>
        private void ResetPriceValue()
        {
            TotalPrice = 0;
            DiscountPercent = 0;
            DiscountPrice = 0;
            Percent = 0;
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            if (Application.Current.MainPage is NavigationPage &&
                (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
            {
                TabbedPage mainPage =
                    (((Application.Current.MainPage as NavigationPage).CurrentPage as FlyoutPage)
                        .Detail as NavigationPage).CurrentPage as TabbedPage;
                mainPage.CurrentPage = mainPage.Children[0];
            }
            else
            {
                _ = await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        #endregion
    }
}