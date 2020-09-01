namespace SPS_Starter.Model
{
    public class Kategorien
    {

        public string Prefix { get; set; }
        public SpsStarter.SpsKategorie Kategorie { get; set; }

        public Kategorien(string prefix, SPS_Starter.Model.SpsStarter.SpsKategorie kategorie)
        {
            Prefix = prefix;
            Kategorie = kategorie;
        }
    }
}
