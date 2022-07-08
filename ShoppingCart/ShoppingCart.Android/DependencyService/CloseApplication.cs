using Android.App;
using habahabamall.DependencyServices;
using habahabamall.Droid.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]

namespace habahabamall.Droid.DependencyService
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            Activity activity = (Activity)Android.App.Application.Context;
            activity.FinishAffinity();
        }
    }
}