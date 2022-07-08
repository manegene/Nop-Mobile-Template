using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Syncfusion.XForms.Android.PopupLayout;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Platform = Xamarin.Essentials.Platform;

namespace habahabamall.Droid
{
    [Activity(Label = "habahabamall", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            SfPopupLayoutRenderer.Init(this);


            LoadApplication(new App());

            // Enable scrolling to the page when the keyboard is enabled
            _ = Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}