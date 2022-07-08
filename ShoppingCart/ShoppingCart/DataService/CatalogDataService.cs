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
    public class CatalogDataService : ICatalogDataService
    {
        private readonly HttpClient httpClient;
        /// <summary>
        /// Creates an instance for the <see cref="CatalogDataService" /> class.
        /// </summary>
        public CatalogDataService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
                         .Accept
                         .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", App.AccessToken);



        }
        /// <summary>
        /// This method is to add the recent product.
        /// </summary>
        public async Task<Status> AddRecentProduct(int userId, int productId)
        {
            Status status = new Status();
            try
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}products/addrecent?userId={userId}&productId={productId}");
                HttpResponseMessage response = await httpClient.PostAsync(uri.ToString(), null);
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        status = JsonConvert.DeserializeObject<Status>(result);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                status.Success = false;
                status.Message = ex.Message;
            }
            catch (Exception ex)
            {
                status.Success = false;
                status.Message = ex.Message;
            }

            return status;
        }

        /// <summary>
        /// This method is to get the recenet/viewed products. 
        /// Feeds the Recommeded Products section
        /// </summary>
        public async Task<List<Product>> GetRecentProductsAsync()
        {
            List<Product> Products = new List<Product>();

            try
            {

                UriBuilder uri = new UriBuilder($"{App.BaseUri}products");
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
        /// To get payment type options.
        /// </summary>
        public async Task<List<Payment>> GetPaymentOptionsAsync()
        {
            List<Payment> Payments = new List<Payment>();
            try
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}checkout/paymentmethods");
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
                        List<PaymentEntity> paymentsEntity = JsonConvert.DeserializeObject<List<PaymentEntity>>(result);
                        if (paymentsEntity != null)
                        {
                            Payments = Mapper.Map<List<PaymentEntity>, List<Payment>>(paymentsEntity);
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

            return Payments;
        }

        /// <summary>
        /// This method is to get the product using id.
        /// </summary>
        public async Task<Product> GetProductByIdAsync(int Id)
        {
            Product Product = new Product();
            try
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}products/{Id}");
                httpClient.DefaultRequestHeaders
                       .Accept
                       .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", App.AccessToken);
                HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        ProductEntity productEntity = JsonConvert.DeserializeObject<ProductEntity>(result);
                        if (productEntity != null)
                        {
                            Product = Mapper.Map<ProductEntity, Product>(productEntity);
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

            return Product;
        }

        /// <summary>
        /// To get product using subcategory id.
        /// </summary>
        public async Task<List<Product>> GetProductBySubCategoryIdAsync(int subCategoryId)
        {
            List<Product> Products = new List<Product>();

            try
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}products/Getprodbycategoryid?subCategoryId={subCategoryId}");
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
        /// Get product reviews from database.
        /// </summary>
        public async Task<List<ProductReviewm>> GetReviewsAsync(int prodId)
        {
            List<ProductReviewm> ProductReview = new List<ProductReviewm>();
            try
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}hbmreviews?productid={prodId}");
                HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        List<ReviewEntity> reviewsEntity = JsonConvert.DeserializeObject<List<ReviewEntity>>(result);
                        if (reviewsEntity != null)
                        {
                            ProductReview = Mapper.Map<List<ReviewEntity>, List<ProductReviewm>>(reviewsEntity);
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

            return ProductReview;
        }

        //get XML formatted prod attributes during order/add to wishlist cart
        public async Task<string> GetxmlProdttributes(int prodid, string attid)
        {
            string attributesString = "";
            if (prodid > 0 && (!string.IsNullOrEmpty(attid)))
            {
                UriBuilder uri = new UriBuilder($"{App.BaseUri}attributesxml?prodid={prodid}{attid}");
                HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        string reviewsEntity = JsonConvert.DeserializeObject<string>(result);
                        if (reviewsEntity != null)
                        {
                            attributesString = reviewsEntity;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Please select some attributes to proceed");
            }
            return attributesString;
        }


    }
}