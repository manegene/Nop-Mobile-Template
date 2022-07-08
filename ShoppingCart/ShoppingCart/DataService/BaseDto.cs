using Newtonsoft.Json;

namespace habahabamall.DataService
{
    public abstract class BaseDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
