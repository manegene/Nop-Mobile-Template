using Newtonsoft.Json;

namespace habahabamall.Models
{
    public class FlutterCustomization
    {

        [JsonProperty("description")]
        public string Title { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

    }
}
