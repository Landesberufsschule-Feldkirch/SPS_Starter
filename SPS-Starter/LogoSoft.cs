using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public void Projekte_Logo8_Lesen()
        {
            // Zuerst die Listen löschen
            StackPanel_Logo8_PLC.Children.Clear();
            StackPanel_Logo8_PLC_Bugs.Children.Clear();

            Button_Logo8_Liste.Add(Button_Starten_Logo8_PLC);
            Button_Logo8_Liste.Add(Button_Starten_Logo8_PLC_Bugs);

            // Name Komplett, kurz, Sprache, Anfang
            List<Tuple<string, string, string>> TupleList_Logo8_PLC = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_Logo8_BUG = new List<Tuple<string, string, string>>();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_Logo8_Quelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                string OrdnerName = d.Name;
                string ProgrammierSprache = "";
                int StartBezeichnung = 0;
                bool Anzeigen = false;

                if (OrdnerName.Contains("FUP"))
                {
                    if (Checkbox_Logo8_FUP.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "FUP";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("FUP");
                }
                if (OrdnerName.Contains("KOP"))
                {
                    if (Checkbox_Logo8_KOP.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "KOP";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("KOP");
                }

                if (Anzeigen)
                {
                    if (d.Name.Contains("PLC"))
                    {
                        // nur PLC und sonst nichts
                        Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                        TupleList_Logo8_PLC.Add(TplEintrag);
                    }
                    else
                    {
                        // Es gibt momentan noch keine Gruppe bei den Bugs
                        Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                        TupleList_Logo8_BUG.Add(TplEintrag);
                    }
                }


            } // Ende foreach

            TupleList_Logo8_PLC.Sort();
            TupleList_Logo8_BUG.Sort();

            Logo8_TabMitInhaltFuellen(TupleList_Logo8_PLC, StackPanel_Logo8_PLC);
            Logo8_TabMitInhaltFuellen(TupleList_Logo8_BUG, StackPanel_Logo8_PLC_Bugs);

            Anzeige_Logo8_Aktualisieren = true;

        }

        private void Logo8_TabMitInhaltFuellen(List<Tuple<string, string, string>> Projekte, System.Windows.Controls.StackPanel StackPanel)
        {
            foreach (Tuple<string, string, string> Projekt in Projekte)
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

            string DateiName = ParentDirectory.FullName + "\\" + Projekt_Logo8_Name + "\\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = LeereHtmlSeite;

            if (Projekt_Logo8_Name.Contains("PLC"))
            {
                Web_Logo8_PLC.NavigateToString(HtmlSeite);
                Web_Logo8_PLC_Bugs.NavigateToString(LeereHtmlSeite);
                return;
            }
            if (Projekt_Logo8_Name.Contains("BUG"))
            {
                Web_Logo8_PLC.NavigateToString(LeereHtmlSeite);
                Web_Logo8_PLC_Bugs.NavigateToString(HtmlSeite);
                return;
            }

        }

    }
}