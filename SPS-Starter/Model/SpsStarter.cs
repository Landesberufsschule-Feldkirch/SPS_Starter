using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace SPS_Starter.Model
{
    public class SpsStarter
    {

        public enum Steuerungen
        {
            Logo,
            TiaPortal,
            TwinCat
        }


        public List<ProjektEigenschaften> AlleProjektEigenschaften = new List<ProjektEigenschaften>();

        public SpsStarter()
        {
            var einstellungen = EinstellungenOrdnerLesen.FromJson(File.ReadAllText(@"Model//Einstellungen.json"));

            OrdnerEinlesen(einstellungen.Logo.Source, einstellungen.Logo.Destination, Steuerungen.Logo);
            OrdnerEinlesen(einstellungen.TiaPortal.Source, einstellungen.TiaPortal.Destination, Steuerungen.TiaPortal);
            OrdnerEinlesen(einstellungen.TwinCat.Source, einstellungen.TwinCat.Destination, Steuerungen.TwinCat);
        }

        private void OrdnerEinlesen(string source, string destination, Steuerungen steuerungen)
        {
            var parentDirectory = new System.IO.DirectoryInfo(source);

            foreach (var ordnerInfo in parentDirectory.GetDirectories())
            {
                if ((ordnerInfo.Attributes & FileAttributes.Directory) != 0 && ordnerInfo.Name != ".git")
                {
                    AlleProjektEigenschaften.Add(new ProjektEigenschaften(steuerungen, ordnerInfo.FullName));
                }
            }
        }
    }



}