namespace SPS_Starter.Model
{
    public class Programmiersprachen
    {

        public string Prefix { get; set; }
        public SpsStarter.SpsSprachen Sprache { get; set; }
        public  string Anzeige { get; set; }

        public Programmiersprachen(string prefix, string anzeige, SPS_Starter.Model.SpsStarter.SpsSprachen sprache)
        {
            Prefix = prefix;
            Anzeige = anzeige;
            Sprache = sprache;
        }
    }
}
