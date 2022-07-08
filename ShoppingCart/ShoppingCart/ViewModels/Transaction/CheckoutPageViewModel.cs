using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Models;
using habahabamall.Views.ErrorAndEmpty;
using habahabamall.Views.Forms;
using habahabamall.Views.Transaction;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;



namespace habahabamall.ViewModels.Transaction
{
    /// <summary>
    /// ViewModel for Checkout page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CheckoutPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CheckoutPageViewModel" /> class.
        /// </summary>
        public CheckoutPageViewModel(IUserDataService userDataService, ICartDataService cartDataService,
            ICatalogDataService catalogDataService, IMyOrdersDataService myOrdersDataService = null)
        {
            this.userDataService = userDataService;
            this.cartDataService = cartDataService;
            this.catalogDataService = catalogDataService;
            DeliveryAddress = new ObservableCollection<Customer>();
            CountryList = new ObservableCollection<Address>();
            ProvinceList = new ObservableCollection<Address>();
            PaymentModes = new ObservableCollection<Payment>();

            Device.BeginInvokeOnMainThread(() =>
            {
                FetchAddresses(App.UserId);
                FetchPaymentOptions();
                FetchCartList();
                ListCountries();
                ListProvinces();

            });
            EditCommand = new DelegateCommand(EditClicked);
            PlaceOrderCommand = new DelegateCommand(PlaceOrderClicked);
            PaymentOptionCommand = new DelegateCommand(PaymentOptionClicked);
            ApplyCouponCommand = new DelegateCommand(ApplyCouponClicked);
            Address_selected = new DelegateCommand(Output);
            AddAddressCommand = new DelegateCommand(AddAddressClicked);
            AddaddressDetails = new DelegateCommand(AddAddress_Details);

        }



        #endregion

        #region Fields
        private bool redirected;
        private string paymentLink;
        private string mpesaPhone;
        private string addressid;
        private bool isPaymentChecked;
        private bool paymentAccepted;
        private string paid;
        private string storeCurrency;
        private bool isAddressChecked;
        private ObservableCollection<Customer> deliveryAddress;

        private ObservableCollection<CartOrWishListProduct> orderedItems = new ObservableCollection<CartOrWishListProduct>();

        private ObservableCollection<Payment> paymentModes;

        private ObservableCollection<CartOrWishListProduct> cartDetails;

        private double totalPrice;

        private double discountPrice;

        private double discountPercent;

        private double percent;
        private readonly IUserDataService userDataService;
        private readonly ICartDataService cartDataService;
        private readonly ICatalogDataService catalogDataService;

        private DelegateCommand backButtonCommand;

        private ObservableCollection<Address> countryList;
        private ObservableCollection<Address> provinceList;
        private string firstName;
        private string lastName;
        private string phone;
        private string address;
        private int countryId;
        private string county;
        private string city;
        private string pcode;
        private object selectedID;
        private object provinceID;
        private int selectedProvinceId;
        //IMyOrdersDataService myOrdersDataService;

        #endregion

        #region Public properties
        public object SelectedID
        {
            get => selectedID;
            set
            {
                selectedID = value;
                OnPropertyChanged();
            }
        }
        public object ProvinceID
        {
            get => provinceID;
            set
            {
                provinceID = value;
                OnPropertyChanged();
            }
        }
        public int SelectedProvinceId
        {
            get => selectedProvinceId;
            set
            {
                selectedProvinceId=value;
                OnPropertyChanged();
            }
        }
        public bool Redirected
        {
            get => redirected;
            set
            {
                redirected = value;
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
        public string PaymentLink
        {
            get => paymentLink;
            set
            {
                if (paymentLink == value)
                {
                    return;
                }
                paymentLink = value;
                OnPropertyChanged();
            }
        }

        public string MpesaPhone
        {
            get => mpesaPhone;
            set
            {
                mpesaPhone = value;
                OnPropertyChanged();
            }
        }
        public bool PaymentAccepted
        {
            get => paymentAccepted;
            set
            {
                paymentAccepted = value;
                OnPropertyChanged();
            }
        }
        public string Paid
        {
            get => paid;
            set
            {
                paid = value;
                OnPropertyChanged();
            }
        }

        public string AddressId
        {
            get => addressid;
            set
            {
                addressid = value;
                OnPropertyChanged();
            }
        }

        public bool IsPaymentChecked
        {
            get => isPaymentChecked;
            set
            {
                //if (isPaymentChecked == value)
                isPaymentChecked = value;
                PaymentSelectedChanged?.Invoke(this, new PropertyChangedEventArgs("IsPaymentChecked"));
            }
        }

        public bool IsAddressChecked
        {
            get => isAddressChecked;
            set
            {
                isAddressChecked = value;
                AddressSelectedChanged?.Invoke(this, new PropertyChangedEventArgs("IsAddressChecked"));
            }
        }


        //address firstname
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }
        //get user address lastname
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }
        //address phone
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }
        //address country id
        public int CountryId
        {
            get => countryId;
            set
            {
                countryId = value;
                OnPropertyChanged();
            }
        }
        //address city
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged();
            }
        }
        //address county
        public string County
        {
            get => county;
            set
            {
                county = value;
                OnPropertyChanged();
            }
        }

        //address postalcode
        public string PCode
        {
            get => pcode;
            set
            {
                pcode = value;
                OnPropertyChanged();
            }

        }
        //address address details
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the property that has been bound with SfListView, which displays the delivery address.
        /// </summary>
        public ObservableCollection<Customer> DeliveryAddress
        {
            get => deliveryAddress;

            set
            {
                if (deliveryAddress == value)
                {
                    return;
                }

                deliveryAddress = value;
                OnPropertyChanged();
            }
        }



        /// <summary>
        /// Gets or sets the property that has been bound with SfListView, which displays the payment modes.
        /// </summary>
        public ObservableCollection<Payment> PaymentModes
        {
            get => paymentModes;

            set
            {
                if (paymentModes == value)
                {
                    return;
                }

                paymentModes = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
        public ObservableCollection<CartOrWishListProduct> OrderedItems
        {
            get => orderedItems;

            set
            {
                if (orderedItems == value)
                {
                    return;
                }

                orderedItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
        public ObservableCollection<CartOrWishListProduct> CartDetails
        {
            get => cartDetails;

            set
            {
                if (cartDetails == value)
                {
                    return;
                }

                cartDetails = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays total price.
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
        /// Gets or sets the property that has been bound with a label, which displays total discount price.
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
        /// Gets or sets the property that has been bound with a label, which displays discount.
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
        public event PropertyChangedEventHandler AddressSelectedChanged;
        public event PropertyChangedEventHandler PaymentSelectedChanged;

        //get list of countrries for use in the adddress regitration
        public ObservableCollection<Address> CountryList
        {
            get => countryList;
            set
            {
                if (countryList == value)
                {
                    return;
                }

                countryList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Address> ProvinceList
        {
            get => provinceList;
            set
            {
                if (provinceList == value)
                {
                    return;
                }

                provinceList = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Command
        /// <summary>
        /// Gets or sets the command that will be executed when the Add new address button is clicked.
        /// </summary>
        public DelegateCommand AddaddressDetails { get; set; }

        //add a new address button
        public DelegateCommand AddAddressCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public DelegateCommand EditCommand { get; set; }


        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public DelegateCommand PlaceOrderCommand { get; set; }

        public DelegateCommand Address_selected { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the payment option button is clicked.
        /// </summary>
        public DelegateCommand PaymentOptionCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the apply coupon button is clicked.
        /// </summary>
        public DelegateCommand ApplyCouponCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand => backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));


        #endregion

        #region Methods


        public async void ListCountries()
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                System.Collections.Generic.List<Address> listofcounties = await userDataService.GetCountries();
                
                CountryList = new ObservableCollection<Address>(listofcounties);
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
        }
        public async void ListProvinces()
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                System.Collections.Generic.List<Address> listofprovinces = await userDataService.GetProvinces();

                ProvinceList = new ObservableCollection<Address>(listofprovinces);
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
        }

        //Add address button is clicked
        /// <summary>
        /// Invoked when the Add address button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>

        public async void AddAddress_Details(object obj)
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                if (SelectedID != null && SelectedID is Address addr && addr != null)
                    CountryId = addr.ID;
                
                if(ProvinceID!=null && ProvinceID is Address addre && addre !=null)
                    SelectedProvinceId = addre.ID;
                   
                if (string.IsNullOrEmpty(City) || string.IsNullOrEmpty(County))
                {
                    await Application.Current.MainPage.DisplayAlert("Validation error", "City/county information cannot be blank", "Cancel");
                }
                 if (string.IsNullOrEmpty(Phone))
                {
                    await Application.Current.MainPage.DisplayAlert("Missing phone number", "Phone number is missing", "Cancel");
                }
                 if (CountryId < 0)
                {
                    await Application.Current.MainPage.DisplayAlert("County is missing", "Select a country from the list", "Cancel");
                }
                if (SelectedProvinceId < 0)
                {
                    await Application.Current.MainPage.DisplayAlert("state is missing", "Select a state from the list", "Cancel");
                }

                else
                {
                    try
                    {
                        Status newaddress = await userDataService.AddAddressDetails(FirstName, LastName, Phone, PCode, City, County, CountryId,SelectedProvinceId, Address, App.Name, App.UserId);
                     
                        await Application.Current.MainPage.DisplayAlert("Success", "Address added successfully", "OK");
                        _ = await App.Current.MainPage.Navigation.PopAsync();
                        await App.Current.MainPage.Navigation.PushAsync(new CheckoutPage());

                    }
                    catch (Exception e)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", e.ToString(), "Cancel");
                    }


                }
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }

        }

        //assign address id on selected address
        public void Output(object address_id)
        {
            if (IsAddressChecked == true)
            {
                //  await  App.Current.MainPage.DisplayAlert("Message", "Selected address", (address_id as SfRadioButton).Text);
                AddressId = (address_id as SfRadioButton).Text;
                // return AddressId;

            }
        }

        /// <summary>
        /// This method is used to get the user address
        /// </summary>
        private async void FetchAddresses(int userId)
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {
                    IsBusy = true;
                    if (App.UserId > 0)
                    {
                        System.Collections.Generic.List<Address> addresses = await userDataService.GetAddresses(App.UserId);


                        if (addresses != null && addresses.Count > 0)
                        {
                            foreach (Address address in addresses)
                            {
                                DeliveryAddress.Add(new Customer
                                {
                                    Addressid = address.ID,
                                    CustomerId = address.UserId,
                                    AddressType = address.Address1,
                                    CustomerName = address.FullName,
                                    MobileNumber = address.MobileNo,
                                    PostalCode = address.PostalCode,
                                    Address = address.Email + " ," + address.Area + " ," + address.City + " ," +
                                              address.County + " ," + address.Country + " ," + address.PostalCode
                                });
                            }
                        }

                        IsBusy = false;
                    }
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Unknown error occured.check network and try again ", "OK");
                }
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
        }

        /// <summary>
        /// This method is used to get the payment options and user card details
        /// </summary>
        private async void FetchPaymentOptions()
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {
                    IsBusy = true;
                    if (App.UserId > 0)
                    {
                        System.Collections.Generic.List<UserCard> userCards = await userDataService.GetUserCardsAsync(App.UserId);
                        if (userCards != null && userCards.Count > 0)
                        {
                            foreach (UserCard userCard in userCards)
                            {
                                PaymentModes.Add(new Payment
                                { PaymentMode = userCard.PaymentMode, CardNumber = userCard.CardNumber });
                            }
                        }
                    }

                    System.Collections.Generic.List<Payment> paymentOptions = await catalogDataService.GetPaymentOptionsAsync();
                    if (paymentOptions != null)
                    {
                        foreach (Payment paymentOption in paymentOptions)
                        {
                            PaymentModes.Add(new Payment
                            { PaymentMode = paymentOption.PaymentMode, CardTypeIcon = paymentOption.CardTypeIcon });
                        }
                    }

                    IsBusy = false;
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "A network error occured", "OK");
                }
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
        }

        /// <summary>
        /// This method is used to get the cart products from database
        /// </summary>
        private async void FetchCartList()
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                IsBusy = true;
                try
                {
                    System.Collections.Generic.List<CartOrWishListProduct> orderedData = await cartDataService.GetOrderedItemsAsync(App.UserId);
                    if (orderedData != null && orderedData.Count > 0)
                    {
                        OrderedItems = new ObservableCollection<CartOrWishListProduct>(orderedData);
                    }

                    System.Collections.Generic.List<CartOrWishListProduct> products = await cartDataService.GetCartOrFavItemAsync(App.UserId, carttype: 1);
                    if (products != null && products.Count > 0)
                    {
                        CartDetails = new ObservableCollection<CartOrWishListProduct>(products);
                        UpdatePrice();

                        //set the store currency.
                        //Its the same currency for all products. Just get the first one in the list
                        StoreCurrency = CartDetails.Select(curr => curr.Currency).FirstOrDefault();
                    }
                    IsBusy = false;
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
        private async void UpdatePaymentLink(string NewLink, ObservableCollection<CartOrWishListProduct> products, string addressid)
        {
            if (!string.IsNullOrEmpty(NewLink))
            {

                await App.Current.MainPage.Navigation.PushModalAsync(new Modal(NewLink, products, addressid));
            }
        }
        /// <summary>
        /// This method is used to update the product total price discount price and percentage
        /// </summary>
        private void UpdatePrice()
        {
            ResetPriceValue();

            if (CartDetails != null && CartDetails.Count > 0)
            {
                foreach (CartOrWishListProduct cartDetail in CartDetails)
                {
                    if (cartDetail.Quantity == 0)
                    {
                        cartDetail.Quantity = 1;
                    }

                    TotalPrice += (cartDetail.OldPrice * cartDetail.Quantity);
                    DiscountPrice += (cartDetail.Price * cartDetail.Quantity);
                    percent += cartDetail.DiscountPrice;
                }
                //finally include shipping feee.
                //currently set at 250 kSH
                DiscountPrice += 250;

                DiscountPercent = percent > 0 ? percent / CartDetails.Count : 0;
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
        /// Invoked when the Edit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        ///button temporarily disable on the DeliveryView.xaml!!!!
        private async void EditClicked(object obj)
        {
            if (App.UserId > 0)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AddAddressView());
            }
        }

        //Add address button is clicked
        /// <summary>
        /// Invoked when the Add address button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>

        private async void AddAddressClicked(object obj)
        {
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                if (App.UserId > 0)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new AddAddressView());
                }
                else if (App.UserId < 0)
                {
                    bool result = await Application.Current.MainPage.DisplayAlert("Error", "You need to be logged in to add a new address", "OK", "Cancel");
                    if (result)
                    {
                        Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                    }
                }
            }
            if (conn != NetworkAccess.Internet || conn == NetworkAccess.Unknown)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());

            }
        }

        /// <summary>
        /// Invoked when the Place order button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void PlaceOrderClicked(object obj)
        {

            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {

                    if (CartDetails != null && CartDetails.Count > 0)
                    {

                        if (IsAddressChecked == false)
                        {
                            await Application.Current.MainPage.DisplayAlert("Validation error", "Select or add a new shipping address", "Cancel");

                        }
                        //address is selected
                        else if (IsAddressChecked == true)
                        {
                            //missing payment option
                            if (string.IsNullOrEmpty(Paid))
                            {
                                await Application.Current.MainPage.DisplayAlert("Payment error", "Missing payment method", "Cancel");
                            }
                            //payment options is selected
                            if (!string.IsNullOrEmpty(Paid))
                            {
                                if (Paid.ToLower().Contains("pay now".ToLower()))
                                {
                                    FlutterPayModel flutterpay = new FlutterPayModel
                                    {
                                        Tx_ref = "order-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                                        Amount = DiscountPrice.ToString(),
                                        Currency = StoreCurrency,
                                        Redirect_url = App.RedirecUrl,
                                        Customer = new FlutterCustomer
                                        {
                                            Email = App.EmailId,
                                            Phonenumber = Phone,
                                            Name = FirstName + " " + LastName
                                        },
                                        Customization = new FlutterCustomization
                                        {
                                            Title = "online checkout",
                                            Logo = $"{App.BaseImageUrl}/uploaded/logo.png"
                                        }


                                    };
                                    IsBusy = true;
                                    FlutterResponse transaction = await userDataService.FlutterPay(flutterpay);

                                    if (transaction.Status == "success")
                                    {
                                        PaymentLink = transaction.Data.Link;
                                        UpdatePaymentLink(PaymentLink, CartDetails, AddressId);

                                        IsBusy = false;

                                    }


                                }
                                //paypal not impleneted for now
                                else if (Paid.ToLower().Contains("paypal".ToLower()))
                                {
                                    throw new Exception("This service is not available right now");
                                }
                                else if (Paid.ToLower().Contains("on delivery".ToLower()))
                                {

                                    PaymentAccepted = await Application.Current.MainPage.DisplayAlert("Cash on delivery", "Will send your " +
                                           "bill while delivering your order. Ensure you have enough cash in your phone " +
                                           " to pay for the order as we dont accept petty cash \n" +
                                           "your total bill will be \n"+ StoreCurrency +":" + DiscountPrice,
                                           "OK", "Cancel");
                                }

                                if (PaymentAccepted == true)
                                {

                                        IsBusy = true;

                                    //user cart items added 
                                    //Api wil handle insert and delete for every product
                                    //await App.Current.MainPage.DisplayAlert("to be sent", App.UserId + "address:" + int.Parse(AddressId) + "paymentmode: cashondelivery","ol");
                                        
                                      var success = await cartDataService.AddOrderedItemAsync(App.UserId, int.Parse(AddressId), paymentmode: "cashondelivery");
                                    if(success.Success == true)
                                    {
                                        IsBusy = false;
                                        await Application.Current.MainPage.Navigation.PushAsync(new PaymentSuccessPage());
                                    }
                                    else if (success.Success == false)
                                    {
                                        IsBusy=false;
                                        await App.Current.MainPage.DisplayAlert("error", "Error completing order.\n Please try again later"+success.Message, "ok");
                                    }


                                }
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Intenet error", ex.Message + "  Unknown network error occured.\n" +
                        " Check your internet and try again \n Payment not successful", "","");
                }
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

        /// <summary>
        /// Invoked when the Payment option is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void PaymentOptionClicked(object obj)
        {
            if (IsPaymentChecked == true)
            {
                //get name of selected payment method
                Paid = (obj as SfRadioButton).Text;

            }

        }

        /// <summary>
        /// Invoked when the Apply coupon button is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ApplyCouponClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}