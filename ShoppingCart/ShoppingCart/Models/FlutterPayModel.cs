using Newtonsoft.Json;

namespace habahabamall.Models
{
    public class FlutterPayModel

    {

        [JsonProperty("tx_ref")]
        public string Tx_ref { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("redirect_url")]
        public string Redirect_url { get; set; }

        [JsonProperty("customer")]
        public FlutterCustomer Customer { get; set; }

        [JsonProperty("customizations")]
        public FlutterCustomization Customization { get; set; }

    }
}
