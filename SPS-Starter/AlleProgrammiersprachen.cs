using System.Windows.Controls;

namespace SPS_Starter
{
    public class AlleProgrammierSprachen
    {
        public string Kurzbezeichnung { get; set; }
        public int Laenge { get; set; }
        public CheckBox CheckBoxBezeichnung { get; set; }

        public AlleProgrammierSprachen(string Kurzbezeichnung, int Laenge, CheckBox CheckBoxBezeichnung)
        {
            this.Kurzbezeichnung = Kurzbezeichnung;
            this.Laenge = Laenge;
            this.CheckBoxBezeichnung = CheckBoxBezeichnung;
        }
    }
}
