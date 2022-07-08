# nop mobile app

###Pre-requisites
1. A running NopCommerce powered store of supported version.--Any version of NopCommerce 4.0 - 4.5. Older versions may work but have not been tested
2. Syncfusion license key --https://www.syncfusion.com/account/downloads
3. A running api. Incase you dont have an api, please contct us for assistance
4. To ue the flutterwave online payment, you need a secret key.-- https://flutterwave.com/

##How to setup the app for your store(remove the curly braces{} when updating fields)
1. Open the App.Xaml.cs file:
 1.1 update the sections{mystore}with your store domain. This is the address your NopCommerce powered store is running.
 e.g. if your store address is https://fashion.store.com, then update the {mystore} with "fashion.store.com" 
 1.2 Update the single field marked {api address} section with your nop web api address.
 1.3 Update the syncfusion license with your license key. Put your key inside the double quotes to be a string value

 
### Finally
Please read the README
Now you are ready to run your store. Clean, build and publish your app
To connect your app with a working api, update the methods in services folder. 
 
