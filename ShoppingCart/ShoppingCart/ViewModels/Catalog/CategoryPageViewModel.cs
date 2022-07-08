using habahabamall.Commands;
using habahabamall.DataService;
using habahabamall.Helpers;
using habahabamall.Models;
using habahabamall.Views.Catalog;
using habahabamall.Views.ErrorAndEmpty;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ScrollToPosition = Syncfusion.ListView.XForms.ScrollToPosition;

namespace habahabamall.ViewModels.Catalog
{
    /// <summary>
    /// ViewModel for Category page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CategoryPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CategoryPageViewModel" /> class.
        /// </summary>
        public CategoryPageViewModel(ICategoryDataService categoryDataService, string selectedCategory)
        {
            this.categoryDataService = categoryDataService;

            Device.BeginInvokeOnMainThread(() =>
           {
               // IsBusy = true;
               // await Task.Delay(30);
               FetchCategories();
               // IsBusy = false;
           });
        }

        #endregion

        #region Public properties

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

        #endregion

        #region Fields

        private ObservableCollection<Category> categories;

        private DelegateCommand scrollToStartCommand;

        private DelegateCommand scrollToEndCommand;

        private DelegateCommand categorySelectedCommand;

        private DelegateCommand expandingCommand;

        private DelegateCommand notificationCommand;
        private readonly ICategoryDataService categoryDataService;

        private DelegateCommand backButtonCommand;

        #endregion

        #region Command       

        /// <summary>
        /// Gets or sets the command that will be executed when the ScrollToStart button is clicked.
        /// </summary>
        public DelegateCommand ScrollToStartCommand =>
            scrollToStartCommand ?? (scrollToStartCommand = new DelegateCommand(ScrollToStartClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the ScrollToEnd button is clicked.
        /// </summary>
        public DelegateCommand ScrollToEndCommand =>
            scrollToEndCommand ?? (scrollToEndCommand = new DelegateCommand(ScrollToEndClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the Category is selected.
        /// </summary>
        public DelegateCommand CategorySelectedCommand =>
            categorySelectedCommand ?? (categorySelectedCommand = new DelegateCommand(CategorySelected));

        /// <summary>
        /// Gets or sets the command that will be executed when expander is expanded.
        /// </summary>
        public DelegateCommand ExpandingCommand =>
            expandingCommand ?? (expandingCommand = new DelegateCommand(ExpanderClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the Notification button is clicked.
        /// </summary>
        public DelegateCommand NotificationCommand =>
            notificationCommand ?? (notificationCommand = new DelegateCommand(NotificationClicked));

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));

        #endregion

        #region Methods

        /// <summary>
        /// Gets the categories from database
        /// </summary>
        /// <param name="selectedCategory">The selectedCategory</param>
        /// no need to get main categories, only subcategories that have products mapped to them
        public async void FetchCategories()
        {
            int retryAttempts = 6;
            TimeSpan delay = TimeSpan.FromSeconds(2);
            NetworkAccess conn = Connectivity.NetworkAccess;
            if (conn == NetworkAccess.Internet)
            {
                try
                {
                    IsBusy = true;

                    //if (string.IsNullOrEmpty(selectedCategory))
                    //{
                    //Main category
                    List<Category> categories = await categoryDataService.GetCategories();

                    if (categories != null && categories.Count > 0)
                    {
                        //await Task.Delay(5000);
                        Categories = new ObservableCollection<Category>(categories);

                    }
                    else if (Categories == null)
                    {
                        RetryHelper.Retry(retryAttempts, delay, () =>
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                FetchCategories();
                            });
                        });
                    }

                    // }
                    /* else
                     {
                         //Sub category
                         isMainCategory = false;

                         var subcategories = await categoryDataService.GetSubCategories(int.Parse(selectedCategory));
                         if (subcategories != null && subcategories.Count > 0)
                         {
                             Categories = new ObservableCollection<Category>(subcategories);
                         }

                     }*/
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

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void ScrollToStartClicked(object attachedObject)
        {
            if (!(attachedObject is SfListView listView))
            {
                return;
            }

            Syncfusion.GridCommon.ScrollAxis.ScrollAxisBase scrollRow = listView.GetVisualContainer()?.ScrollRows;
            int firstVisibleIndex = (int)scrollRow?.ScrollLineIndex;
            int totalItemsCount = listView.DataSource.DisplayItems.Count;

            int scrollToIndex = firstVisibleIndex > 0 && firstVisibleIndex < totalItemsCount - 1 ? firstVisibleIndex - 1 : 0;
            listView.LayoutManager.ScrollToRowIndex(scrollToIndex, ScrollToPosition.Center,
                true);
        }

        /// <summary>
        /// Invoked when the items are sorted.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private static void ScrollToEndClicked(object attachedObject)
        {
            if (!(attachedObject is SfListView listView))
            {
                return;
            }

            Syncfusion.GridCommon.ScrollAxis.ScrollAxisBase scrollRow = listView.GetVisualContainer()?.ScrollRows;
            int lastVisibleIndex = (int)scrollRow?.LastBodyVisibleLineIndex;
            int totalItemsCount = listView.DataSource.DisplayItems.Count;

            int scrollToIndex = lastVisibleIndex >= 0 && lastVisibleIndex < totalItemsCount - 1 ? lastVisibleIndex + 1 : totalItemsCount - 1;
            listView.LayoutManager.ScrollToRowIndex(scrollToIndex, ScrollToPosition.Center,
                true);
        }

        /// <summary>
        /// Invoked when the Category is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void CategorySelected(object attachedObject)
        {
            Category category = attachedObject as Category;
            /* if (category == null && attachedObject is string)
             {
                 IsBusy = true;

                 await Application.Current.MainPage.Navigation.PushAsync(new CatalogListPage(string.Empty));
                 IsBusy = false;
                 return;

             }*/

            if (category.ID > 0)
            {
                IsBusy = true;

                //  if (isMainCategory)
                // {
                //   await Application.Current.MainPage.Navigation.PushAsync(
                //      new CategoryTilePage(category.ID.ToString()));
                // }
                // else
                // {
                await Application.Current.MainPage.Navigation.PushAsync(
                    new CatalogListPage(category.ID));
                // }

                IsBusy = false;

                return;
            }

            await Application.Current.MainPage.DisplayAlert("Error",
                $"Select any one category or selected category {attachedObject} not found", "Close");
        }

        /// <summary>
        /// Invoked when the expander is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ExpanderClicked(object obj)
        {
            List<object> objects = obj as List<object>;
            Category category = objects[0] as Category;

            if (!(objects[1] is SfListView listView))
            {
                return;
            }

            int itemIndex = listView.DataSource.DisplayItems.IndexOf(category);
            int scrollIndex = itemIndex + category.SubCategories.Count;
            //Expand and bring the item in the view.
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(100);
                listView.LayoutManager.ScrollToRowIndex(scrollIndex, ScrollToPosition.End,
                    true);
            });
        }

        /// <summary>
        /// Invoked when the notification button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void NotificationClicked(object obj)
        {
            // Do something
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