using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SPS_Starter
{
    public class SharedDisplay
    {
        public void StackpanelAktualisieren(List<AlleEigenschaften> alleEigenschaften, List<Button> btnListe)
        {
            foreach (var Eigenschaften in alleEigenschaften)
            {
                Eigenschaften.ProjekteBezeichnung.Clear();// Zuerst die Listen löschen
                Eigenschaften.StackPanelBezeichnung?.Children?.Clear(); // Anzeige löschen
                btnListe.Add(Eigenschaften.ButtonBezeichnung);
            }
        }

        public void ProgrammiersprachenAktualisieren(List<AlleProgrammierSprachen> alleProgrammierSprachen, List<AlleEigenschaften> alleEigenschaften, string ordnerQuelle)
        {
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ordnerQuelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {

                foreach (var Programmiersprachen in alleProgrammierSprachen)
                {
                    if (d.Name.Contains(Programmiersprachen.Kurzbezeichnung))
                    {
                        if (Programmiersprachen.CheckBoxBezeichnung.IsChecked.Value)
                        {
                            string ProgrammierSprache = Programmiersprachen.Kurzbezeichnung;
                            int StartBezeichnung = Programmiersprachen.Laenge + d.Name.IndexOf(Programmiersprachen.Kurzbezeichnung);
                            foreach (var Eigenschaften in alleEigenschaften)
                            {
                                string ProjektTeilBezeichnung = Eigenschaften.Kurzbezeichnung + "_" + Programmiersprachen.Kurzbezeichnung;
                                if (d.Name.Contains(ProjektTeilBezeichnung))
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

        public void AnzeigenAktualisieren(MainWindow mainWindow, List<AlleEigenschaften> alleEigenschaften)
        {
            string KurzbezeichnungSprache;

            foreach (var Eigenschaften in alleEigenschaften)
            {
                List<Tuple<string, string, string>> EindeutigeListe = Eigenschaften.ProjekteBezeichnung.Distinct().ToList();

                foreach (Tuple<string, string, string> Projekt in EindeutigeListe)
                {
                    if (Projekt.Item2 == "CPP") KurzbezeichnungSprache = "C++";
                    else KurzbezeichnungSprache = Projekt.Item2;

                    RadioButton rdo = new RadioButton
                    {
                        GroupName = Eigenschaften.GruppenName,
                        Name = Projekt.Item3,
                        FontSize = 14,
                        Content = Projekt.Item1 + " (" + KurzbezeichnungSprache + ")",
                        VerticalAlignment = VerticalAlignment.Top
                    };

                    if (Eigenschaften.GruppenName == "Logo8") rdo.Checked += new RoutedEventHandler(mainWindow.gLogo8.RadioButton_Checked);
                    if (Eigenschaften.GruppenName == "TwinCAT") rdo.Checked += new RoutedEventHandler(mainWindow.gTwinCat.RadioButton_Checked);
                    if (Eigenschaften.GruppenName == "TIA_PORTAL_V14_SP1") rdo.Checked += new RoutedEventHandler(mainWindow.gTiaPortal.RadioButton_Checked);

                    Eigenschaften.StackPanelBezeichnung.Children.Add(rdo);
                }
            }
        }

        public void HtmlFeldFuellen(MainWindow mainWindow, List<AlleProgrammierSprachen> alleProgrammierSprachen, List<AlleEigenschaften> alleEigenschaften, List<Button> btnListe, string orderQuelle, string btnBeschriftung)
        {
            string HtmlSeite;
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(orderQuelle);

            ListeDarstellungAendern(btnListe, true, Colors.Green, btnBeschriftung);

            string DateiName = $@"{ParentDirectory.FullName}\{mainWindow.gProjekt_Name}\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = mainWindow.gHtmlSeiteDateiFehlt;

            HtmlInhaltAnzeigen(mainWindow, alleProgrammierSprachen, alleEigenschaften, HtmlSeite);
        }


        private void ListeDarstellungAendern(List<Button> btnListe, bool enable, Color Farbe, string text)
        {
            foreach (Button Knopf in btnListe)
            {
                Knopf.IsEnabled = enable;
                Knopf.Background = new SolidColorBrush(Farbe);
                Knopf.Content = text;
                Knopf.Refresh();
            }
        }

        public void HtmlInhaltAnzeigen(MainWindow mainWindow, List<AlleProgrammierSprachen> alleProgrammierSprachen, List<AlleEigenschaften> alleEigenschaften, string htmlSeite)
        {
            string ProgrammierSprache = "";

            foreach (var Programmiersprachen in alleProgrammierSprachen)
            {
                if (mainWindow.gProjekt_Name.Contains(Programmiersprachen.Kurzbezeichnung)) ProgrammierSprache = Programmiersprachen.Kurzbezeichnung;
            }

            byte[] dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
            MemoryStream stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            byte[] dataLeereHtmlSeite = Encoding.UTF8.GetBytes(mainWindow.gHtmlSeiteLeer);
            MemoryStream stmLeereHtmlSeite = new MemoryStream(dataLeereHtmlSeite, 0, dataLeereHtmlSeite.Length);

            foreach (var EigenSchaften in alleEigenschaften)
            {
                if (mainWindow.gProjekt_Name.Contains(EigenSchaften.Kurzbezeichnung + "_" + ProgrammierSprache)) EigenSchaften.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);
                else EigenSchaften.BrowserBezeichnung.NavigateToStream(stmLeereHtmlSeite);
            }
        }
    }
}
