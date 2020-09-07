using Newtonsoft.Json;

namespace SPS_Starter.Model
{
    public class Logo
    {
        [JsonProperty("Source")]
        public string Source { get; set; }

        [JsonProperty("Destination")]
        public string Destination { get; set; }
    }
}