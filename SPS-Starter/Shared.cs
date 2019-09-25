using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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
        }

        public void ProjektbezeichnungenStackpanelLeerenAktualisieren(ObservableCollection<AlleEigenschaften> AlleEigenschaften, List<Button> ButtonListe)
        {
            foreach (var Eigenschaften in AlleEigenschaften)
            {
                Eigenschaften.ProjekteBezeichnung.Clear();// Zuerst die Listen löschen
                Eigenschaften.StackPanelBezeichnung.Children.Clear(); // Anzeige löschen
            }

            foreach (var Eigenschaften in AlleEigenschaften)
            {
                ButtonListe.Add(Eigenschaften.ButtonBezeichnung);
            }
        }

        public void ProgrammiersprachenListeAktualisieren(string Ordner, ObservableCollection<AlleProgrammierSprachen> AlleProgrammierSprachen, ObservableCollection<AlleEigenschaften> AlleEigenschaften)
        {
            string ProgrammierSprache = "";
            int StartBezeichnung = 0;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(Ordner);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {

                foreach (var Programmiersprachen in AlleProgrammierSprachen)
                {
                    if (d.Name.Contains(Programmiersprachen.Kurzbezeichnung))
                    {
                        if (Programmiersprachen.CheckBoxBezeichnung.IsChecked.Value)
                        {
                            ProgrammierSprache = Programmiersprachen.Kurzbezeichnung;
                            StartBezeichnung = Programmiersprachen.Laenge + d.Name.IndexOf(Programmiersprachen.Kurzbezeichnung);

                            foreach (var Eigenschaften in AlleEigenschaften)
                            {
                                if (d.Name.Contains(Eigenschaften.Kurzbezeichnung))
                                {
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(d.Name.Substring(StartBezeichnung), ProgrammierSprache, d.Name);
                                    Eigenschaften.ProjekteBezeichnung.Add(TplEintrag);
                                }
                            }
                        }
                    }
                }
            }

        }

        public void AnzeigeAktualisieren(ObservableCollection<AlleEigenschaften> AlleEigenschaften)
        {
            foreach (var Eigenschaften in AlleEigenschaften)
            {
                Eigenschaften.ProjekteBezeichnung.Sort();

                List<Tuple<string, string, string>> EindeutigeListe = Eigenschaften.ProjekteBezeichnung.Distinct().ToList();

                foreach (Tuple<string, string, string> Projekt in EindeutigeListe)
                {
                    RadioButton rdo = new RadioButton
                    {
                        GroupName = Eigenschaften.GruppenName,
                        Name = Projekt.Item3,
                        FontSize = 14,
                        Content = Projekt.Item1 + " (" + Projekt.Item2 + ")",
                        VerticalAlignment = VerticalAlignment.Top
                    };

                    if (Eigenschaften.GruppenName == "Logo8!")
                    {
                        rdo.Checked += new RoutedEventHandler(Logo8_radioButton_Checked);
                    }

                    if (Eigenschaften.GruppenName == "TwinCAT")
                    {
                        rdo.Checked += new RoutedEventHandler(TwinCAT_radioButton_Checked);
                    }

                    if (Eigenschaften.GruppenName == "TIA_PORTAL_V14_SP1")
                    {
                        rdo.Checked += new RoutedEventHandler(TiaPortal_radioButton_Checked);
                    }

                    Eigenschaften.StackPanelBezeichnung.Children.Add(rdo);
                }

            }
        }

        public void WebBrowserFuellen(string Beschriftung, string ProjektOrdner, string ProjektName, ObservableCollection<AlleEigenschaften> AlleEigenschaften)
        {
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner);

            DarstellungAendernListe(Button_TiaPortal_Liste, true, Colors.Green, Beschriftung);

            string DateiName = $@"{ParentDirectory.FullName}\{ProjektName}\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = "<!doctype html>   </html >";

            byte[] dataHtmlSeite = Encoding.UTF8.GetBytes(HtmlSeite);
            MemoryStream stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            byte[] dataLeereHtmlSeite = Encoding.UTF8.GetBytes(LeereHtmlSeite);
            MemoryStream stmLeereHtmlSeite = new MemoryStream(dataLeereHtmlSeite, 0, dataLeereHtmlSeite.Length);

            foreach (var EigenSchaften in AlleEigenschaften)
            {
                if (ProjektName.Contains(EigenSchaften.Kurzbezeichnung)) EigenSchaften.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);
                else EigenSchaften.BrowserBezeichnung.NavigateToStream(stmLeereHtmlSeite);
            }
        }

    }
}