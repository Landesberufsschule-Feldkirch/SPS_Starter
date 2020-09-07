using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SPS_Starter.Model
{
    // https://app.quicktype.io/#l=cs&r=json2csharp
    public partial class EinstellungenOrdnerLesen
    {
        [JsonProperty("Logo")]
        public Logo Logo { get; set; }

        [JsonProperty("TiaPortal")]
        public Logo TiaPortal { get; set; }

        [JsonProperty("TwinCAT")]
        public Logo TwinCat { get; set; }
    }

    public class Logo
    {
        [JsonProperty("Source")]
        public string Source { get; set; }

        [JsonProperty("Destination")]
        public string Destination { get; set; }
    }

    public partial class EinstellungenOrdnerLesen
    {
        public static EinstellungenOrdnerLesen FromJson(string json) => JsonConvert.DeserializeObject<EinstellungenOrdnerLesen>(json, Converter.Settings);
    }
    
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
