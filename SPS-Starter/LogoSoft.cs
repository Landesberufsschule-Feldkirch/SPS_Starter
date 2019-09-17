using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text;
using System.Linq;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public void Projekte_Logo8_Lesen()
        {
            foreach (var Eigenschaften in Eigenschaften_Logo8)
            {
                Eigenschaften.ProjekteBezeichnung.Clear();// Zuerst die Listen löschen
                Eigenschaften.StackPanelBezeichnung.Children.Clear(); // Anzeige löschen
            }

            foreach (var Eigenschaften in Eigenschaften_Logo8)
            {
                Button_Logo8_Liste.Add(Eigenschaften.ButtonBezeichnung);
            }
            
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_Logo8_Quelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                string OrdnerName = d.Name;
                string ProgrammierSprache = "";
                int StartBezeichnung = 0;

                foreach (var Programmiersprachen in AlleProgrammierSprachen_Logo8)
                {
                    if (OrdnerName.Contains(Programmiersprachen.Kurzbezeichnung))
                    {
                        if (Programmiersprachen.CheckBoxBezeichnung.IsChecked.Value)
                        {
                            ProgrammierSprache = Programmiersprachen.Kurzbezeichnung;
                            StartBezeichnung = Programmiersprachen.Laenge + OrdnerName.IndexOf(Programmiersprachen.Kurzbezeichnung);

                            foreach (var Eigenschaften in Eigenschaften_Logo8)
                            {
                                if (d.Name.Contains(Eigenschaften.Kurzbezeichnung))
                                {
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                                    Eigenschaften.ProjekteBezeichnung.Add(TplEintrag);
                                }
                            }
                        }
                    }
                }
            } 

            foreach (var Eigenschaften in Eigenschaften_Logo8)
            {
                Eigenschaften.ProjekteBezeichnung.Sort();
                Logo8_TabMitInhaltFuellen(Eigenschaften.ProjekteBezeichnung, Eigenschaften.StackPanelBezeichnung);
            }

            Anzeige_Logo8_Aktualisieren = true;
        }

        private void Logo8_TabMitInhaltFuellen(List<Tuple<string, string, string>> Projekte, System.Windows.Controls.StackPanel StackPanel)
        {
            List<Tuple<string, string, string>> EindeutigeListe = Projekte.Distinct().ToList();

            foreach (Tuple<string, string, string> Projekt in EindeutigeListe)
            {
                RadioButton rdo = new RadioButton
                {
                    GroupName = "Logo8!",
                    Name = Projekt.Item3,
                    FontSize = 14,
                    Content = Projekt.Item1 + " (" + Projekt.Item2 + ")",
                    VerticalAlignment = VerticalAlignment.Top
                };
                rdo.Checked += new RoutedEventHandler(Logo8_radioButton_Checked);
                StackPanel.Children.Add(rdo);
            }
        }

        private void Logo8_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            Projekt_Logo8_Name = rb.Name;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_Logo8_Quelle);

            DarstellungAendernListe(Button_Logo8_Liste, true, Colors.Green, "Logo Projekt starten");

            string DateiName = $@"{ParentDirectory.FullName}\{Projekt_Logo8_Name}\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = LeereHtmlSeite;

            byte[] dataHtmlSeite = Encoding.UTF8.GetBytes(HtmlSeite);
            MemoryStream stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            byte[] dataLeereHtmlSeite = Encoding.UTF8.GetBytes(LeereHtmlSeite);
            MemoryStream stmLeereHtmlSeite = new MemoryStream(dataLeereHtmlSeite, 0, dataLeereHtmlSeite.Length);

            foreach (var EigenSchaften in Eigenschaften_Logo8)
            {
                if (Projekt_Logo8_Name.Contains(EigenSchaften.Kurzbezeichnung)) EigenSchaften.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);
                else EigenSchaften.BrowserBezeichnung.NavigateToStream(stmLeereHtmlSeite);
            }
        }
    }
}