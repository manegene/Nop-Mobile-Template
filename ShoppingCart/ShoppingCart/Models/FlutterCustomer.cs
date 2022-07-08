using Newtonsoft.Json;

namespace habahabamall.Models
{
    public class FlutterCustomer
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phonenumber")]
        public string Phonenumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
