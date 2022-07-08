using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.Views.Bookmarks;
using habahabamall.Views.ErrorAndEmpty;
using habahabamall.Views.Forms;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.Detail
{
    /// <summary>
    /// ViewModel for detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DetailPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="DetailPageViewModel" /> class.
        /// </summary>
        public DetailPageViewModel(ICatalogDataService catalogDataService, ICartDataService cartDataService,
              int selectedProduct)
        {
            this.catalogDataService = catalogDataService;
            this.cartDataService = cartDataService;
            this.selectedProduct = selectedProduct;

            Device.BeginInvokeOnMainThread(() =>
            {
                //disable add recent products method on product selection
                //   AddRecentProduct(selectedProduct.ID.ToString());
                FetchProduct(selectedProduct);
                UpdateCartItemCount();
            });
            Attribute_selected = new DelegateCommand(SelectedAttributes);
            AddFavouriteCommand = new DelegateCommand(AddFavouriteClicked);
            NotificationCommand = new DelegateCommand(NotificationClicked);
            AddToCartCommand = new DelegateCommand(AddToCartClicked);
            LoadMoreCommand = new DelegateCommand(LoadMoreClicked);
            ShareCommand = new DelegateCommand(ShareClicked);
            VariantCommand = new DelegateCommand(VariantClicked);
            CardItemCommand = new DelegateCommand(CartClicked);
            BackButtonCommand = new DelegateCommand(BackButtonClicked);
        }


        #endregion

        #region Fields
        private string selectedAttributeId;
        private double productRating;
        public string attributeStringParams;

        // private string zoomimage;
        private Product productDetail;

        private readonly int selectedProduct;

        private ObservableCollection<Category> categories;
        private bool isFavourite;

        private bool isReviewVisible;
        private bool isAttributeChecked;
        private int? cartItemCount;
        private readonly ICatalogDataService catalogDataService;
        private readonly ICartDataService cartDataService;

        #endregion

        #region Public properties
        public event PropertyChangedEventHandler AttSelected;
        public DelegateCommand Attribute_selected { get; set; }

        //will compile all ids of selected attributes before contructing the query string
        public List<string> SelectedAttIds = new List<string>();

        //will be appended to the get attributes query during add to cart/wishlist
        public string AttributeStringParams
        {
            get => attributeStringParams;
            set
            {
                if (attributeStringParams == value)
                {
                    return;
                }
                attributeStringParams = value;
                OnPropertyChanged();
            }
        }
        public bool IsAttributeChecked
        {
            get => isAttributeChecked;
            set
            {
                if (isAttributeChecked == value)
                {
                    return;
                }
                isAttributeChecked = value;
                AttSelected?.Invoke(this, new PropertyChangedEventArgs("IsAttributeChecked"));
            }
        }

        public string SelectedAttributeId
        {
            get => selectedAttributeId;
            set
            {
                if (selectedAttributeId == value)
                {
                    return;
                }
                selectedAttributeId = value;
                OnPropertyChanged();
            }
        }
        /* public string ZoomImage
         {
             get => zoomimage;
             set
             {
                // if (zoomimage == value)
                //// {
                 //    return;
                // }
                 zoomimage = value;
                 OnPropertyChanged();
             }
         }*/
        /// <summary>
        /// Gets or sets the property that has been bound with SfRotator and labels, which display the product images and
        /// details.
        /// </summary>

        public Product ProductDetail
        {
            get => productDetail;

            set
            {
                /* if (productDetail == value)
                 {
                     return;
                 }*/

                productDetail = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with StackLayout, which displays the categories using ComboBox.
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get => categories;

            set
            {
                if (categories == value)
                {
                    return;
                }

                categories = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the Favourite.
        /// </summary>
        public bool IsFavourite
        {
            get => isFavourite;
            set
            {
                isFavourite = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the empty message.
        /// </summary>
        public bool IsReviewVisible
        {
            get
            {
                if (productDetail != null && productDetail.Reviews != null)
                {
                    if (productDetail.Reviews.Count == 0)
                    {
                        isReviewVisible = true;
                    }
                }

                return isReviewVisible;
            }
            set
            {
                isReviewVisible = value;
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

        #region Commands

        /// <summary>
        /// Gets or sets the command that will be executed when the Favourite button is clicked.
        /// </summary>
        public DelegateCommand AddFavouriteCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Notification button is clicked.
        /// </summary>
        public DelegateCommand NotificationCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the AddToCart button is clicked.
        /// </summary>
        public DelegateCommand AddToCartCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Show All button is clicked.
        /// </summary>
        public DelegateCommand LoadMoreCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Share button is clicked.
        /// </summary>
        public DelegateCommand ShareCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the size button is clicked.
        /// </summary>
        public DelegateCommand VariantCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when cart button is clicked.
        /// </summary>
        public DelegateCommand CardItemCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand { get; set; }

        #endregion

        #region Methods

        //method called to add and format attributes string with selected attributes ids
        public void SelectedAttributes(object obj)
        {
            if (IsAttributeChecked)
            {
                SelectedAttributeId = (obj as SfCheckBox).ClassId;

                if (SelectedAttributeId != null)
                {
                    SelectedAttIds.Add(SelectedAttributeId);

                }



            }
            //remove unselected checkbox
            else if (!IsAttributeChecked)
            {
                SelectedAttributeId = (obj as SfCheckBox).ClassId;

                if (SelectedAttributeId != null)
                {
                     SelectedAttIds.Remove(SelectedAttributeId);

                }
            }
            AttributeStringParams = "&attributeid=" + string.Join("&attributeid=", SelectedAttIds.ToList());

        }


        /// <summary>
        /// This method is used to add the recent product to the database.
        /// </summary>
        /// <param name="productId"></param>
        public async void AddRecentProduct(string productId)
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {
                    if (App.UserId > 0)
                    {
                        Status status = await catalogDataService.AddRecentProduct(App.UserId, int.Parse(productId));
                        if (status != null && !status.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Message", "System Error adding recent product", "OK");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
        }

        /// <summary>
        /// This method is used to update the cart item count.
        /// </summary>
        public async void UpdateCartItemCount()
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
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
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "unknown network error occured", "OK");
                }
            }

        }

        /// <summary>
        /// This method is used to get the product based on product id.
        /// </summary>
        /// <param name="selectedProduct">Product id</param>
        public async void FetchProduct(int selectedProduct)
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {
                    Product product = await catalogDataService.GetProductByIdAsync(selectedProduct);
                    if (product != null)
                    {
                        ProductDetail = GetProductDetail(product);

                        List<CartOrWishListProduct> wishlistProducts = await cartDataService.GetCartOrFavItemAsync(App.UserId, carttype: 2);
                        if (wishlistProducts != null && wishlistProducts.Count > 0)
                        {
                            ProductDetail.IsFavourite = wishlistProducts.Any(s => s.ID == ProductDetail.ID);
                        }

                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
        }

        /// <summary>
        /// This method is used to update the product reviews and ratings.
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns></returns>
        public Product GetProductDetail(Product product)
        {
            _ = new Product();
            Product selectedPoductDetail = product;

            if (selectedPoductDetail.DiscountAppliedToProducts > 0)
            {
                selectedPoductDetail.IsDiscountPositive = true;
            }

            if (selectedPoductDetail.Reviews == null || selectedPoductDetail.Reviews.Count == 0)
            {
                IsReviewVisible = true;
            }
            else
            {
                foreach (ProductReviewm review in selectedPoductDetail.Reviews)
                {
                    productRating += review.Rating;
                }
            }


            if (productRating > 0)
            {
                selectedPoductDetail.ApprovedRatingSum = productRating / selectedPoductDetail.Reviews.Count;
            }

            return selectedPoductDetail;

        }

        /// <summary>
        /// Invoked when the Favourite button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddFavouriteClicked(object obj)
        {
            Status status = new Status();
            string ReponseAttributes = "";

            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {
                    if (App.UserId > 0)
                    {
                        if (obj != null && obj is DetailPageViewModel model && model != null)
                        {
                            //query wish if such a prod exist for the user
                            var checkwish = await cartDataService.GetCartOrFavItemAsync(App.UserId, 2);
                            if (checkwish != null)
                            {
                                model.ProductDetail.IsFavourite =! checkwish.Any(prd => prd.ID == model.ProductDetail.ID);
                            }

                            if (ProductDetail.HasAttributes && model.ProductDetail.IsFavourite==true)
                            {
                                ReponseAttributes = await catalogDataService.GetxmlProdttributes(model.ProductDetail.ID, AttributeStringParams);

                                if (!string.IsNullOrEmpty(ReponseAttributes))
                                {
                                    status = await cartDataService.AddCartOrFavItemAsync(App.UserId,
                                   model.ProductDetail.ID, ReponseAttributes,model.ProductDetail.IsFavourite,2 );
                                }
                            }
                            else if (!ProductDetail.HasAttributes)
                            {
                                status = await cartDataService.AddCartOrFavItemAsync(App.UserId,
                                model.ProductDetail.ID, ReponseAttributes, model.ProductDetail.IsFavourite,2 );
                            }

                            if (status == null && !status.Success)
                            {
                                model.ProductDetail.IsFavourite = !model.ProductDetail.IsFavourite;
                            }
                            else if (status != null && status.Success)
                            {
                                IsFavourite = ProductDetail.IsFavourite;
                            }
                        }
                    }
                    else
                    {
                        var result = await Application.Current.MainPage.DisplayAlert("Message",
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

        }

        /// <summary>
        /// Invoked when the Notification button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void NotificationClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddToCartClicked(object obj)
        {
            string ReponseAttributes = "";
            Status status = new Status();
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {
                    if (App.UserId > 0)
                    {
                        if (obj != null && obj is DetailPageViewModel detailPageViewModel && detailPageViewModel != null)
                        {
                            Product product = detailPageViewModel.ProductDetail;
                            IsBusy = true;

                            if (product.HasAttributes)
                            {
                                ReponseAttributes = await catalogDataService.GetxmlProdttributes(product.ID, AttributeStringParams);

                                if (!string.IsNullOrEmpty(ReponseAttributes))
                                {
                                    status = await cartDataService.AddCartOrFavItemAsync(App.UserId, product.ID, ReponseAttributes, isfavorite: true, carttype: 1);

                                }
                            }
                            else if (!product.HasAttributes)
                            {
                                status = await cartDataService.AddCartOrFavItemAsync(App.UserId, product.ID, ReponseAttributes, isfavorite: true, carttype: 1);

                            }

                            if (status != null && status.Success)
                            {
                                await Application.Current.MainPage.DisplayAlert("succes", "Product added to cart successfully", "Ok");
                                CartItemCount++;
                            }
                            else if (status != null && !status.Success)
                            {
                                bool result = await Application.Current.MainPage.DisplayAlert("Alert",
                                    "This item has been already added in cart", "Go to Cart", " ");
                                if (result)
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
                                }
                            }
                            IsBusy = false;

                        }
                    }
                    else
                    {
                        bool result = await Application.Current.MainPage.DisplayAlert("Message",
                            "Please login to add the product on your cart.", "OK", "CANCEL");
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
        }

        /// <summary>
        /// Invoked when Load more button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoadMoreClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the Share button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ShareClicked(object obj)
        {
            if (obj != null && obj is DetailPageViewModel model && model != null)
            {
                string prodname = model.ProductDetail.SeoPname;

                await Share.RequestAsync(new ShareTextRequest
                {
                    Title = model.ProductDetail.Pname,
                    Uri = model.productDetail.Uri + prodname,
                    Text = "this is available at: "
                });
            }
        }

        /// <summary>
        /// Invoked when the variant button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void VariantClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when cart icon button is clicked.
        /// </summary>
        /// <param name="obj"></param>
        private async void CartClicked(object obj)
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
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
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

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