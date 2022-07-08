using habahabamall.DependencyServices;
using habahabamall.iOS.DependencyService;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]

namespace habahabamall.iOS.DependencyService
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            Thread.CurrentThread.Abort();
        }
    }
}