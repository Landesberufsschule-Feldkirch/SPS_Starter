namespace SPS_Starter.Model
{
    public class Programmiersprachen
    {

        public string Prefix { get; set; }
        public SpsStarter.SpsSprachen Sprache { get; set; }

        public Programmiersprachen(string prefix, SpsStarter.SpsSprachen sprache)
        {
            Prefix = prefix;
            Sprache = sprache;
        }
    }
}
