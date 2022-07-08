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
    /// Data service to load the data from database using web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CategoryDataService : ICategoryDataService
    {
        #region fields


        private readonly HttpClient httpClient;
        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="CategoryDataService" /> class.
        /// </summary>
        public CategoryDataService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
                       .Accept
                       .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", App.AccessToken);

        }

        #endregion

        #region Methods

        /// <summary>
        /// To get product  parent categories.
        /// </summary>
        public async Task<List<Category>> GetCategories()
        {
            List<Category> Categories = null;
            try
            {
                //await and get access token first
                // await Task.Delay(2000);

                UriBuilder uri = new UriBuilder($"{App.BaseUri}products/categories");
                httpClient.DefaultRequestHeaders
                           .Accept
                           .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", App.AccessToken);

                HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        List<CategoryEntity> categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(result);
                        if (categories != null)
                        {
                            Categories = Mapper.Map<List<CategoryEntity>, List<Category>>(categories);
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

            return Categories;
        }

        /// <summary>
        /// This method is to get the subcategories using category id.
        /// </summary>
       /* public async Task<List<Category>> GetSubCategories(int categoryId)
        {
            List<Category> Categories = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/categories?categoryId={categoryId}");
                httpClient.DefaultRequestHeaders
                           .Accept
                           .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", App.AccessToken);

                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(result);
                        if (categories != null)
                        {
                            Categories = Mapper.Map<List<CategoryEntity>, List<Category>>(categories);
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

            return Categories;
        }*/

        #endregion
    }
}