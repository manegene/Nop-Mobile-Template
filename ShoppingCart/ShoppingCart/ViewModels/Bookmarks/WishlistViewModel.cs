using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.Views.Bookmarks;
using habahabamall.Views.Detail;
using habahabamall.Views.ErrorAndEmpty;
using habahabamall.Views.Forms;
using habahabamall.Views.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.Bookmarks
{
    /// <summary>
    /// ViewModel for wishlist page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class WishlistViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="WishlistViewModel" /> class.
        /// </summary>
        public WishlistViewModel( ICartDataService cartDataService)
        {
            this.cartDataService = cartDataService;
        }

        #endregion

        #region Fields
        private DelegateCommand itemSelectedCommand;

        private ObservableCollection<CartOrWishListProduct> wishlistDetails;

        private double totalPrice;

        private double discountPrice;

        private double discountPercent;

        private double percent;

        private bool isEmptyViewVisible;

        private int? cartItemCount;

        private DelegateCommand cardItemCommand;

        private Command addToCartCommand;

        private DelegateCommand deleteCommand;

        private DelegateCommand quantitySelectedCommand;
        private readonly ICartDataService cartDataService;

        private DelegateCommand backButtonCommand;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the wishlist details.
        /// </summary>
        public ObservableCollection<CartOrWishListProduct> WishlistDetails
        {
            get => wishlistDetails;

            set
            {
                if (wishlistDetails == value)
                {
                    return;
                }

                wishlistDetails = value;
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
        /// Gets or sets the property that has been bound with view, which displays the cart items count.
        /// </summary>
        public int? CartItemCount
        {
            get => cartItemCount;

            set
            {
                cartItemCount = value;
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
        /// Gets the command will be executed when the cart button has been clicked.
        /// </summary>
        public DelegateCommand CardItemCommand =>
            cardItemCommand ?? (cardItemCommand = new DelegateCommand(CartClicked));

        /// <summary>
        /// Gets the command that will be executed when the AddToCart button is clicked.
        /// </summary>
        public Command AddToCartCommand => addToCartCommand ?? (addToCartCommand = new Command(AddToCartClicked));

        /// <summary>
        /// Gets the command that will be executed when the Remove button is clicked.
        /// </summary>
        public DelegateCommand DeleteCommand => deleteCommand ?? (deleteCommand = new DelegateCommand(DeleteClicked));

        /// <summary>
        /// Gets the command that will be executed when the quantity is selected.
        /// </summary>
        public DelegateCommand QuantitySelectedCommand =>
            quantitySelectedCommand ?? (quantitySelectedCommand = new DelegateCommand(QuantitySelected));

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));

        #endregion

        #region Methods
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
        /// This method is used to get the wish list items
        /// </summary>
        public async void FetchWishlist()
        {
            try
            {
                if (App.UserId > 0)
                {
                    List<CartOrWishListProduct> wishlistProducts = await cartDataService.GetCartOrFavItemAsync(App.UserId, carttype: 2);

                    if (wishlistProducts != null && wishlistProducts.Count > 0)
                    {
                        IsEmptyViewVisible = false;
                        WishlistDetails = new ObservableCollection<CartOrWishListProduct>(wishlistProducts);
                        foreach (CartOrWishListProduct wishlist in WishlistDetails)
                        {
                            if (wishlist.DiscountAppliedToProducts > 0)
                            {
                                wishlist.IsDiscountPositive = true;
                            }

                            wishlist.Quantities = new List<object> { "1", "2", "3" };
                        }

                    }
                    else
                    {
                        IsEmptyViewVisible = true;
                    }
                }
                else
                {
                    bool result = await Application.Current.MainPage.DisplayAlert("Message",
                        "Please login to view your wishlist items", "OK", "CANCEL");
                    if (result)
                    {
                        Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to get the cart item count in the wishlist view
        /// 
        /// </summary>
        public async void UpdateCartItemCount()
        {
            try
            {
                if (App.UserId > 0)
                {
                    List<CartOrWishListProduct> cartItems = await cartDataService.GetCartOrFavItemAsync(App.UserId, carttype: 1);
                    if (cartItems != null)
                    {
                        CartItemCount = cartItems.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void CartClicked(object obj)
        {
            if (CartItemCount > 0)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EmptyCartPage(false));
            }
        }

        /// <summary>
        /// Invoked when add to cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddToCartClicked(object obj)
        {
            try
            {
                if (App.UserId > 0)
                {
                    if (obj != null && obj is CartOrWishListProduct product && product != null)
                    {
                        if (product.HasAttributes==true)
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new DetailPage(product.ID));
                        }
                        else
                        {


                            Status status = await cartDataService.AddCartOrFavItemAsync(App.UserId, product.ID, attributes:"", isfavorite: true, carttype: 1);
                            if (status != null && status.Success)
                            {
                                CartItemCount = CartItemCount + 1;
                                bool result = await Application.Current.MainPage.DisplayAlert("Success",
                                    $"The item {product.Pname} has been added to cart", "Go to Cart", " ");
                                if (result)
                                {
                                    if (product.DiscountAppliedToProducts > 0)
                                    {
                                        product.IsDiscountPositive = true;
                                    }

                                    await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
                                }

                            }
                            else if (status != null && !status.Success)
                            {
                                bool result = await Application.Current.MainPage.DisplayAlert("Alert",
                                    "This item has been already added in cart", "Go to Cart", " ");
                                if (result)
                                {
                                    if (product.DiscountAppliedToProducts > 0)
                                    {
                                        product.IsDiscountPositive = true;
                                    }

                                    await Application.Current.MainPage.Navigation.PushAsync(new CartPage());

                                }
                            }
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
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when an delete button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void DeleteClicked(object obj)
        {
            try
            {
                if (obj != null && obj is CartOrWishListProduct product && WishlistDetails.Count > 0)
                {
                    _ = WishlistDetails.Remove(product);
                    _ = await cartDataService.AddCartOrFavItemAsync(App.UserId, product.ID, attributes: "", isfavorite: false, carttype: 2);
                    if (WishlistDetails.Count == 0)
                    {
                        IsEmptyViewVisible = true;
                    }
                    else if (IsEmptyViewVisible)
                    {
                        IsEmptyViewVisible = false;
                    }
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unknown error cocured", "OK");
            }
        }

        /// <summary>
        /// Invoked when the quantity is selected.
        /// </summary>
        /// <param name="selectedItem">The Object</param>
        private void QuantitySelected(object selectedItem)
        {
            UpdatePrice();
        }

        /// <summary>
        /// This method is used to update the price amount.
        /// </summary>
        private void UpdatePrice()
        {
            ResetPriceValue();

            if (WishlistDetails != null && WishlistDetails.Count > 0)
            {
                foreach (CartOrWishListProduct wishlistDetail in WishlistDetails)
                {
                    if (wishlistDetail.TotalQuantity == 0)
                    {
                        wishlistDetail.TotalQuantity = 1;
                    }

                    TotalPrice += wishlistDetail.OldPrice * wishlistDetail.TotalQuantity;
                    DiscountPrice += wishlistDetail.DiscountPrice * wishlistDetail.TotalQuantity;
                    percent += wishlistDetail.DiscountAppliedToProducts;
                }

                DiscountPercent = percent > 0 ? percent / WishlistDetails.Count : 0;
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
            percent = 0;
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