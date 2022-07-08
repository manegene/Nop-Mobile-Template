using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Helpers;
using habahabamall.Models;
using habahabamall.Views.Detail;
using habahabamall.Views.ErrorAndEmpty;
using habahabamall.Views.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace habahabamall.ViewModels.Catalog
{
    /// <summary>
    /// ViewModel for home page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ProductHomePageViewModel : BaseViewModel
    {
        #region Constructor
        public ProductHomePageViewModel() : this(productHomeDataService, catalogDataService)
        {
        }
        public ProductHomePageViewModel(IProductHomeDataService productHomeDataService,
            ICatalogDataService catalogDataService)
        {
            App.ListenNetworkChanges();

            ProductHomePageViewModel.productHomeDataService = productHomeDataService;
            ProductHomePageViewModel.catalogDataService = catalogDataService;

            Device.BeginInvokeOnMainThread(() =>
            {
                FetchRecentProducts();
                //FetchOfferProducts();
            });

            itemSelectedCommand = new DelegateCommand(this.ItemSelected);
        }

        #endregion

        #region Fields

        private Command<object> nextCommand;
        private int selectedFeatured;

        private ObservableCollection<Product> newArrivalProduts;

        private ObservableCollection<Product> offerProduts;

        private ObservableCollection<Product> recommendedProduts;

        private DelegateCommand itemSelectedCommand;


        private DelegateCommand viewAllCommand;

        private DelegateCommand masterPageOpenCommand;

        private string bannerImage;

        private static IProductHomeDataService productHomeDataService;
        private static ICatalogDataService catalogDataService;

        private bool isRecentProductExists;
        private bool isfeaturedExists;

        #endregion

        #region Public properties

        public int SelectedFeatured
        {
            get => selectedFeatured;
            set
            {
                selectedFeatured = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the property that has been bound with Xamarin.Forms Image, which displays the banner image.
        /// </summary>
        [DataMember(Name = "bannerimage")]
        public string BannerImage
        {
            // get => App.BaseImageUrl + bannerImage;
            get => App.BaseImageUrl + "0000209_default3_100.png";
            set
            {
                bannerImage = value;
                OnPropertyChanged();
                // FetchOfferProducts();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of products from json.
        /// </summary>
        public ObservableCollection<Product> NewArrivalProducts
        {
            get => newArrivalProduts;

            set
            {
                if (newArrivalProduts == value)
                {
                    return;
                }

                newArrivalProduts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of products from json.
        /// </summary>
        public ObservableCollection<Product> OfferProducts
        {
            get => offerProduts;

            set
            {
                if (offerProduts == value)
                {
                    return;
                }

                offerProduts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of products from json.
        /// </summary>
        public ObservableCollection<Product> RecommendedProducts
        {
            get => recommendedProduts;

            set
            {
                if (recommendedProduts == value)
                {
                    return;
                }

                recommendedProduts = value;
                OnPropertyChanged();
            }
        }

        public bool IsRecentProductExists
        {
            get => isRecentProductExists;
            set
            {
                isRecentProductExists = value;
                OnPropertyChanged();
            }
        }

        public bool IsFeaturedExists
        {
            get => isfeaturedExists;
            set
            {
                isfeaturedExists = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        public Command<object> NextCommand
        {
            get => nextCommand;
            set => nextCommand = value;
        }
        public ICommand ItemTappedCommand { get; }
        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public DelegateCommand ItemSelectedCommand =>
            itemSelectedCommand ?? (itemSelectedCommand = new DelegateCommand(ItemSelected));

        public DelegateCommand ViewAllCommand =>
            viewAllCommand ?? (viewAllCommand = new DelegateCommand(ViewAllProducts));

        public DelegateCommand MasterPageOpenCommand =>
            masterPageOpenCommand ?? (masterPageOpenCommand = new DelegateCommand(MasterPageOpened));

        #endregion

        #region Methods



        /// <summary>
        /// This method is used to get the banner images
        /// </summary>
        public async void FetchBannerImage()
        {
            try
            {
                List<Banner> banners = await productHomeDataService.GetBannersAsync();
                if (banners != null && banners.Count > 0)
                {
                    BannerImage = banners.FirstOrDefault().BannerImage;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to get the recent products
        /// </summary>
        public async void FetchRecentProducts()
        {
            int retryAttempts = 3;
            TimeSpan delay = TimeSpan.FromSeconds(2);
            NetworkAccess conn = Connectivity.NetworkAccess;
            List<Product> Featured = new List<Product>();
            try
            {

                IsBusy = true;

                List<Product> recommends = await catalogDataService.GetRecentProductsAsync();
                if (recommends != null && recommends.Count > 0)
                {
                    foreach (Product prod in recommends)
                    {
                        if (prod.DiscountAppliedToProducts > 0)
                        {
                            prod.IsDiscountPositive = true;

                        }

                    }
                   
                    NewArrivalProducts = new ObservableCollection<Product>(recommends.Where(newprod => newprod.IsNewProduct == true).OrderBy(prodid => prodid.ID));
                    if(NewArrivalProducts!=null)
                        IsFeaturedExists= true;

                    IsRecentProductExists = true;
                    RecommendedProducts = new ObservableCollection<Product>(recommends);

                    IsBusy = false;
                }

                else if (RecommendedProducts == null)
                {
                    RetryHelper.Retry(retryAttempts, delay, () =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            FetchRecentProducts();
                        });


                    });
                }



            }
            catch (Exception)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
            // }

        }


        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private async void ItemSelected(object attachedObject)
        {
            IsBusy = true;
            if (attachedObject != null && attachedObject is Product product && product != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(product.ID));
            }

            IsBusy = false;
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void MasterPageOpened(object attachedObject)
        {
            if (Application.Current.MainPage is NavigationPage &&
                (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
            {
                ((Application.Current.MainPage as NavigationPage).CurrentPage as FlyoutPage).IsPresented = true;
            }
        }

        /// <summary>
        /// Invoked when an view all is clicked.
        /// </summary>
        /// <param name="obj"></param>
        private void ViewAllProducts(object obj)
        {
            //Do something
        }

        #endregion
    }
}