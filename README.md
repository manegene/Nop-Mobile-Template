# This is a working Xamarin forms e-commerce Android & IoS mobile template developed on top of Syncfusion's E-cart sample. You can customize the app and use it for your ecommerce store. Incase you use nopCommerce, there is an API solution already existing- please reach out. Otherwise, you need to develop your API and update the endoints to conform to your API endpoints.
The main purpose of this project is to lessen the development time for anyone looking for  a mobile solution to their e-commerce store.
The template is designed to use a secure endpoint(firebase secured) with data persistence
 
The Shopping Cart is an online shopping application developed using [Syncfusion’s Xamarin UI controls](https://www.syncfusion.com/xamarin-ui-controls) and  [Essential UI Kit](https://github.com/syncfusion/essential-ui-kit-for-xamarin.forms) on the Xamarin.Forms platform.

## working complete application [Playstore](https://play.google.com/store/apps/details?id=com.habahabamall.ShoppingCartApp)

## Sponsor this project:  [![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?hosted_button_id=XMQSX7J83V5AN)
 

## Features integrated 

* Display product in categories
* Product details
* Wishlist
* Cart
* Checkout
* Order History
* Contact Us page
* About Us page

This project uses the following patterns and features:

* XAML UI
* Converters
* Custom controls
* Data binding
* Behaviors
* MVVM
* Styles
 
## Syncfusion controls

This project uses the following Syncfusion controls:

* [Button](https://www.syncfusion.com/xamarin-ui-controls/xamarin-button)
* [Cards](https://www.syncfusion.com/xamarin-ui-controls/xamarin-cards)
* [Expander](https://www.syncfusion.com/xamarin-ui-controls/xamarin-expander)
* [Badge View](https://www.syncfusion.com/xamarin-ui-controls/xamarin-badge-view)
* [Busy Indicator](https://www.syncfusion.com/xamarin-ui-controls/xamarin-busy-indicator)
* [ComboBox](https://www.syncfusion.com/xamarin-ui-controls/xamarin-combobox)
* [ListView](https://www.syncfusion.com/xamarin-ui-controls/xamarin-listview)
* [Maps](https://www.syncfusion.com/xamarin-ui-controls/xamarin-maps)
* [Rating](https://www.syncfusion.com/xamarin-ui-controls/xamarin-rating)
* [Rotator](https://www.syncfusion.com/xamarin-ui-controls/xamarin-rotator)

## Screens

**Android**

<img src="images/ProductHomeScreen_Android.png" Width="190" /> <img src="images/ProductDetailScreen_Android.png" Width="190" /> <img src="images/EmptyWishList_Android.png" Width="190" /> 
<img src="images/ProductCartScreen_Android.png" Width="190" /><img src="images/Review.jpg" Width="190" /><img src="images/checkout.jpg" Width="190" />

**iOS**

<img src="images/ProductHomeScreen_iOS.png" Width="190" /> <img src="images/ProductDetailScreen_iOS.png" Width="190" /> <img src="images/EmptyWishList_iOS.png" Width="190" />
<img src="images/ProductCartScreen_iOS.png" Width="190" /><img src="images/Review.jpg" Width="190" /><img src="images/checkout.jpg" Width="190" />




## Requirements to run the sample

* Visual Studio 2017 and later
* Xamarin add-ons for Visual Studio (available via the Visual Studio installer)

## How to run 

1. Clone the sample and open it in Visual Studio.
2. Register your license key in App.cs as shown below.

        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");

            InitializeComponent();

            MainPage = new MasterDetail();
        } 

Please refer to this [link](https://help.syncfusion.com/common/essential-studio/licensing/license-key#xamarinforms) for more information about Syncfusion licenses.

3. Set any one of the platform-specific projects (iOS, Android) as a startup project.
4. Clean and build the application.
5. Run the application.
 
