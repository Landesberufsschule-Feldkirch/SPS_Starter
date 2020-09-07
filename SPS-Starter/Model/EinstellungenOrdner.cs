using Newtonsoft.Json;

namespace SPS_Starter.Model
{
    // https://app.quicktype.io/#l=cs&r=json2csharp

    public partial class EinstellungenOrdnerLesen
    {
        public static EinstellungenOrdnerLesen FromJson(string json) => JsonConvert.DeserializeObject<EinstellungenOrdnerLesen>(json, Converter.Settings);
    }
}
