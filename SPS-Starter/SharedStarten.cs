using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        private void ProStarten(object sender, RoutedEventArgs e)
        {
            string OrdnerQuelle = "-";
            string OrdnerZiel = "-";
            string ProjektOeffnenMit = "";
            string ProjektName = "";
            List<Button> Button_Liste = new List<Button>();

            Button StartKnopf = sender as Button;

            switch (StartKnopf?.Content.ToString())
            {
                case "Logo Projekt starten":
                    OrdnerQuelle = gLogo8.OrdnerQuelle;
                    OrdnerZiel = gLogo8.OrdnerZiel;
                    Button_Liste = gLogo8.ButtonListe;
                    ProjektName = gProjekt_Name;
                    ProjektOeffnenMit = "Projekt mit Logo8 öffnen";
                    break;

                case "TiaPortal Projekt starten":
                    OrdnerQuelle = gTiaPortal.OrdnerQuelle;
                    OrdnerZiel = gTiaPortal.OrdnerZiel;
                    Button_Liste = gTiaPortal.ButtonListe;
                    ProjektName = gProjekt_Name;
                    ProjektOeffnenMit = "Projekt mit TiaPortal öffnen";
                    break;

                case "TwinCAT Projekt starten":
                    OrdnerQuelle = gTwinCat.OrdnerQuelle;
                    OrdnerZiel = gTwinCat.OrdnerZiel;
                    Button_Liste = gTwinCat.ButtonListe;
                    ProjektName = gProjekt_Name;
                    ProjektOeffnenMit = "Projekt mit TwinCAT öffnen";
                    break;

                default:
                    MessageBox.Show("Unbekannter Knopf: " + StartKnopf.Content);
                    break;
            }

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(OrdnerQuelle);
            string sourceDirectory = $@"{ParentDirectory.FullName}\{ProjektName}";

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Ordner " + OrdnerZiel + " löschen");
                if (System.IO.Directory.Exists(OrdnerZiel)) System.IO.Directory.Delete(OrdnerZiel, true);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 2 caught.");
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Ordner " + OrdnerZiel + " erstellen");
                System.IO.Directory.CreateDirectory(OrdnerZiel);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 3 caught.");
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Alle Dateien kopieren");
                Copy(sourceDirectory, OrdnerZiel);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 4 caught.");
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.LawnGreen, ProjektOeffnenMit);
                Process proc = new Process();
                proc.StartInfo.FileName = OrdnerZiel + "\\start.cmd";
                proc.StartInfo.WorkingDirectory = OrdnerZiel;
                proc.Start();
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 5 caught.");
            }
        }
    }
}
