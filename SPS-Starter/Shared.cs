using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        private void DarstellungAendernListe(List<Button> KnopfListe, bool Enable, Color Farbe, string Text)
        {
            foreach (Button Knopf in KnopfListe)
            {
                Knopf.IsEnabled = Enable;
                Knopf.Background = new SolidColorBrush(Farbe);
                Knopf.Content = Text;
                Knopf.Refresh();
            }
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine($@"Copying {target.FullName}\{fi.Name}");
                fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void ProjektStarten(object sender, RoutedEventArgs e)
        {
            string ProjektOeffnenMit = "";
            string ProjektOrdnerQuelle = "";
            string ProjektOrdnerZiel = "";
            string ProjektName = "";
            List<Button> Button_Liste = new List<Button>();

            Button StartKnopf = sender as Button;

            switch (StartKnopf?.Content.ToString())
            {
                case "Logo Projekt starten":
                    ProjektOrdnerQuelle = ProjektOrdner_Logo8_Quelle;
                    ProjektOrdnerZiel = Projekt_Logo8_Ziel;
                    Button_Liste = Button_Logo8_Liste;
                    ProjektName = Projekt_Logo8_Name;
                    ProjektOeffnenMit = "Projekt mit Logo8! öffnen";
                    break;

                case "TiaPortal Projekt starten":
                    ProjektOrdnerQuelle = ProjektOrdner_TiaPortal_Quelle;
                    ProjektOrdnerZiel = Projekt_TiaPortal_Ziel;
                    Button_Liste = Button_TiaPortal_Liste;
                    ProjektName = Projekt_TiaPortal_Name;
                    ProjektOeffnenMit = "Projekt mit TiaPortal öffnen";
                    break;

                case "TwinCAT Projekt starten":
                    ProjektOrdnerQuelle = ProjektOrdner_TwinCAT_Quelle;
                    ProjektOrdnerZiel = Projekt_TwinCAT_Ziel;
                    Button_Liste = Button_TwinCAT_Liste;
                    ProjektName = Projekt_TwinCAT_Name;
                    ProjektOeffnenMit = "Projekt mit TwinCAT öffnen";
                    break;

                default:
                    MessageBox.Show("Unbekannter Knopf: " + StartKnopf.Content);
                    break;
            }

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdnerQuelle);
            string sourceDirectory = $@"{ParentDirectory.FullName}\{ProjektName}";

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Ordner " + ProjektOrdnerZiel + " löschen");
                if (System.IO.Directory.Exists(ProjektOrdnerZiel)) System.IO.Directory.Delete(ProjektOrdnerZiel, true);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 2 caught.");
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Ordner " + ProjektOrdnerZiel + " erstellen");
                System.IO.Directory.CreateDirectory(ProjektOrdnerZiel);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 3 caught.");
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Alle Dateien kopieren");
                Copy(sourceDirectory, ProjektOrdnerZiel);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 4 caught.");
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.LawnGreen, ProjektOeffnenMit);
                Process proc = new Process();
                proc.StartInfo.FileName = ProjektOrdnerZiel + "\\start.cmd";
                proc.StartInfo.WorkingDirectory = ProjektOrdnerZiel;
                proc.Start();
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 5 caught.");
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl; // e.Source could have been used instead of sender as well
            TabItem item = tabControl.SelectedValue as TabItem;
            switch (item.Header.ToString())
            {
                case "Logo8!":
                    if (Anzeige_Logo8_Aktualisieren) Projekte_Logo8_Lesen();
                    break;

                case "TiaPortal":
                    if (Anzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
                    break;

                case "TwinCAT":
                    if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;

                default:
                    break;

            }
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {

            CheckBox cb = sender as CheckBox;

            switch (cb.Name)
            {
                case "Checkbox_Logo8_FUP":
                    if (Anzeige_Logo8_Aktualisieren) Projekte_Logo8_Lesen();
                    break;
                case "Checkbox_Logo8_KOP":
                    if (Anzeige_Logo8_Aktualisieren) Projekte_Logo8_Lesen();
                    break;

                case "Checkbox_TiaPortal_FUP":
                    if (Anzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
                    break;
                case "Checkbox_TiaPortal_KOP":
                    if (Anzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
                    break;
                case "Checkbox_TiaPortal_SCL":
                    if (Anzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
                    break;


                case "Checkbox_TwinCAT_AS":
                    if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_AWL":
                    if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_CFC":
                    if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_FUP":
                    if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_KOP":
                    if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_ST":
                    if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;

                default:
                    break;

            }

            if (Anzeige_Logo8_Aktualisieren) Projekte_Logo8_Lesen();
        }
        
    }
}