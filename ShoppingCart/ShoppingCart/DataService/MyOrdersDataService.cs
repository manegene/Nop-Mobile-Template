using habahabamall.ViewModels.History;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Xamarin.Forms.Internals;

namespace habahabamall.DataService
{
    /// <summary>
    /// Data service to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class MyOrdersDataService
    {
        #region Constructor

        #endregion

        #region Methods

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            string file = "habahabamall.Data." + fileName;

            Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            T obj;

            using (System.IO.Stream stream = assembly.GetManifestResourceStream(file))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(stream);
            }

            return obj;
        }

        #endregion

        #region fields

        private static MyOrdersDataService instance;

        private MyOrdersPageViewModel myOrderPageViewModel;

        #endregion

        #region Properties

        /// <summary>
        /// Gets an instance of the <see cref="MyOrdersDataService" />.
        /// </summary>
        public static MyOrdersDataService Instance => instance ?? (instance = new MyOrdersDataService());

        /// <summary>
        /// Gets or sets the value of my orders page view model.
        /// </summary>
        public MyOrdersPageViewModel MyOrdersPageViewModel =>
            myOrderPageViewModel = PopulateData<MyOrdersPageViewModel>("ecommerce.json");

        #endregion
    }
}