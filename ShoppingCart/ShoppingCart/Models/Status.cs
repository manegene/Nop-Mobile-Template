using System.Collections.Generic;

namespace habahabamall.Models
{
    public class Status
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IReadOnlyCollection<string> Errors { get; set; }
        public string Name { get; set; }

        //mpesa gateway responses
        public string access_token { get; set; }
        public string MerchantRequestID { get; set; }
        public string CheckoutRequestID { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string CustomerMessage { get; set; }
        public string errorMessage { get; set; }

    }
}