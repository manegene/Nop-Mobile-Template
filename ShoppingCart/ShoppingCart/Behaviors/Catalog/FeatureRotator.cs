using habahabamall.Models;
using habahabamall.ViewModels.Catalog;
using habahabamall.Views.Detail;
using Syncfusion.SfRotator.XForms;
using System;
using Xamarin.Forms;

namespace habahabamall.Behaviors.Catalog
{
    public class FeatureRotator : Behavior<SfRotator>
    {
        #region properties
        private SfRotator navigationRotator;
        private readonly ProductHomePageViewModel viewModel = new ProductHomePageViewModel();

        #endregion
        protected override void OnAttachedTo(SfRotator bindable)
        {
            navigationRotator = bindable;
            navigationRotator.ItemTapped += Featured_Itemtapped;
            base.OnAttachedTo(bindable);

        }

        protected override void OnDetachingFrom(SfRotator bindable)
        {
            base.OnDetachingFrom(bindable);
            navigationRotator.ItemTapped -= Featured_Itemtapped;
        }
        private async void Featured_Itemtapped(object sender, EventArgs e)
        {

            try
            {
                Product prod = viewModel.NewArrivalProducts[navigationRotator.SelectedIndex];

                if (prod != null && prod is Product)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(prod.ID));
                }
            }
            catch (Exception)
            {
                _ = await Application.Current.MainPage.DisplayAlert("A temporary error prevented loading your item.", "please check again shortly", " ", " ");
            }

        }
    }
}
