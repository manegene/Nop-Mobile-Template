using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.Views.Bookmarks;
using habahabamall.Views.Detail;
using habahabamall.Views.ErrorAndEmpty;
using habahabamall.Views.Forms;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.Catalog
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CatalogPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CatalogPageViewModel" /> class.
        /// </summary>
        public CatalogPageViewModel(ICatalogDataService catalogDataService, ICartDataService cartDataService,
              int selectedCategory)
        {
            this.catalogDataService = catalogDataService;
            this.cartDataService = cartDataService;

            Device.BeginInvokeOnMainThread(() =>
            {
                UpdateCartItemCount();
                FetchProducts(selectedCategory);

            });
        }

        #endregion

        #region Fields        

        private ObservableCollection<Product> products;

        private DelegateCommand addFavouriteCommand;

        private DelegateCommand itemSelectedCommand;

        public DelegateCommand cardItemCommand;

        private int cartItemCount;
        private readonly ICatalogDataService catalogDataService;
        private readonly ICartDataService cartDataService;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the item details in tile.
        /// </summary>
        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the cart items count.
        /// </summary>
        public int CartItemCount
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
        /// Gets or sets the command that will be executed when the Favourite button is clicked.
        /// </summary>
        public DelegateCommand AddFavouriteCommand =>
            addFavouriteCommand ?? (addFavouriteCommand = new DelegateCommand(AddFavouriteClicked));

        /// <summary>
        /// Gets or sets the command will be executed when the cart icon button has been clicked.
        /// </summary>
        public DelegateCommand CardItemCommand =>
            cardItemCommand ?? (cardItemCommand = new DelegateCommand(CartClicked));

        #endregion

        #region Methods

        /// <summary>
        /// This method is used to get the products based on category id
        /// </summary>
        /// <param name="selectedCategory"></param>
        public async void FetchProducts(int selectedCategory)
        {
            try
            {
                IsBusy = true;
                //int subCategoryId;
                //int.TryPars(selectedCategory, out subCategoryId);
                System.Collections.Generic.List<Product> products = await catalogDataService.GetProductBySubCategoryIdAsync(selectedCategory);
                if (products != null && products.Count > 0)
                {
                    foreach (Product prod in products)
                    {
                        if (prod.DiscountAppliedToProducts > 0)
                        {
                            prod.IsDiscountPositive = true;
                        }
                    }

                    Products = new ObservableCollection<Product>(products);

                    System.Collections.Generic.List<CartOrWishListProduct> wishlistProducts = await cartDataService.GetCartOrFavItemAsync(App.UserId, carttype: 2);
                    if (wishlistProducts != null && wishlistProducts.Count > 0)
                    {
                        foreach (Product product in Products.Where(a => wishlistProducts.Any(s => s.ID == a.ID)))
                        {
                            product.IsFavourite = true;
                        }
                    }
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to update the cart item count.
        /// </summary>
        public async void UpdateCartItemCount()
        {
            try
            {
                if (App.UserId > 0)
                {
                    System.Collections.Generic.List<CartOrWishListProduct> cartItems = await cartDataService.GetCartOrFavItemAsync(App.UserId, carttype: 1);
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
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private async void ItemSelected(object attachedObject)
        {
            if (attachedObject != null && attachedObject is Product product && product != null)
            {
                if (product.DiscountAppliedToProducts > 0)
                {
                    product.IsDiscountPositive = true;
                }
                int prodid = product.ID;

                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(prodid));
            }

        }

        /// <summary>
        /// Invoked when the favourite button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddFavouriteClicked(object obj)
        {
            string attributes = "";
            try
            {
                if (App.UserId > 0)
                {
                    IsBusy = true;
                    if (obj != null && obj is Product product && product != null)
                    {
                        product.IsFavourite = !product.IsFavourite;
                        bool isFavourite = product.IsFavourite;

                        Status status = await cartDataService.AddCartOrFavItemAsync(App.UserId, product.ID,
                                 attributes, isfavorite: isFavourite, carttype: 2);
                        if (status == null || !status.Success)
                        {
                            product.IsFavourite = !isFavourite;
                        }
                    }
                    IsBusy = false;
                }
                else
                {
                    bool result = await Application.Current.MainPage.DisplayAlert("Message",
                        "Please login to add your favorite item.", "OK", "CANCEL");
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
        /// Invoked when cart icon button is clicked.
        /// </summary>
        /// <param name="obj"></param>
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

        #endregion
    }
}