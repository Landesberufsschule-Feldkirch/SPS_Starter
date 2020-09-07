using Newtonsoft.Json;

namespace SPS_Starter.Model
{
    public partial class EinstellungenOrdnerLesen
    {
        [JsonProperty("Logo")]
        public Logo Logo { get; set; }

        [JsonProperty("TiaPortal")]
        public Logo TiaPortal { get; set; }

        [JsonProperty("TwinCAT")]
        public Logo TwinCat { get; set; }
    }
}