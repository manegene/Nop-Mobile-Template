using AutoMapper;
using habahabamall.Models;
using Newtonsoft.Json;
using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace habahabamall.DataService
{
    /// <summary>
    /// Data service to load the data from database using Web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProductHomeDataService : IProductHomeDataService
    {
        #region fields


        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="ProductHomeDataService" /> class.
        /// </summary>
        public ProductHomeDataService()
        {
            httpClient = new HttpClient();
        }


        #endregion

        #region Methods

        /// <summary>
        /// To get the offer product from database.
        /// </summary>
        public async Task<List<Product>> GetOfferProductsAsync()
        {
            List<Product> Products = new List<Product>();
            try
            {

                UriBuilder uri = new UriBuilder($"{App.BaseUri}products/featuredproducts");

                httpClient.DefaultRequestHeaders
                       .Accept
                       .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", App.AccessToken);
                // await App.Current.MainPage.DisplayAlert("mmh", App.AccessToken, "cancel");
                HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        List<ProductEntity> productsEntity = JsonConvert.DeserializeObject<List<ProductEntity>>(result);
                        if (productsEntity != null)
                        {
                            Products = Mapper.Map<List<ProductEntity>, List<Product>>(productsEntity);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Products;
        }

        /// <summary>
        /// To get the banner image.
        /// </summary>
        public async Task<List<Banner>> GetBannersAsync()
        {
            List<Banner> Banners = new List<Banner>();
            try
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}products/banner");
                HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        List<BannerEntity> bannersEntity = JsonConvert.DeserializeObject<List<BannerEntity>>(result);
                        if (bannersEntity != null)
                        {
                            Banners = Mapper.Map<List<BannerEntity>, List<Banner>>(bannersEntity);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Banners;
        }

        #endregion
    }
}