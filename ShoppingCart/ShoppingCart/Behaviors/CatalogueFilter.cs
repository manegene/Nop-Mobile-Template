using habahabamall.Models;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace habahabamall.Behaviors
{
    public class CatalogueFilter : Behavior<ContentPage>
    {
        #region Fields

        private SfListView ListView;
        private SearchBar SearchBar;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("productsmainlist");
            SearchBar = bindable.FindByName<SearchBar>("prodfilterText");
            SearchBar.TextChanged += SearchBar_TextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            SearchBar.TextChanged -= SearchBar_TextChanged;
            SearchBar = null;
            ListView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region Methods

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListView.DataSource != null)
            {
                ListView.DataSource.Filter = Filterproducts;
                ListView.DataSource.RefreshFilter();
            }
            ListView.RefreshView();
        }

        private bool Filterproducts(object obj)
        {
            if (SearchBar == null || SearchBar.Text == null)
            {
                return true;
            }

            Product taskInfo = obj as Product;
            string Prodinfo = "";

            if (taskInfo.ShortDescription != null)
            {
                Prodinfo = taskInfo.ShortDescription;
            }
            return
                (taskInfo.Pname.ToLower().Contains(SearchBar.Text.ToLower())
                || Prodinfo.ToLower().Contains(SearchBar.Text.ToLower()));
        }
        #endregion
    }
}
