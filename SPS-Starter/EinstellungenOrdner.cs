namespace SPS_Starter
{
    // https://app.quicktype.io/#l=cs&r=json2csharp

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Globalization;

    public partial class EinstellungenOrdnerLesen
    {
        [JsonProperty("Logo")]
        public Logo Logo { get; set; }

        [JsonProperty("TiaPortal")]
        public Logo TiaPortal { get; set; }

        [JsonProperty("TwinCAT")]
        public Logo TwinCat { get; set; }
    }

    public partial class Logo
    {
        [JsonProperty("SteuerungsType")]
        public string SteuerungsType { get; set; }

        [JsonProperty("Source")]
        public string Source { get; set; }

        [JsonProperty("Destination")]
        public string Destination { get; set; }
    }

    public partial class EinstellungenOrdnerLesen
    {
        public static EinstellungenOrdnerLesen FromJson(string json) => JsonConvert.DeserializeObject<EinstellungenOrdnerLesen>(json, SPS_Starter.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this EinstellungenOrdnerLesen self) => JsonConvert.SerializeObject(self, SPS_Starter.Converter.Settings);
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
